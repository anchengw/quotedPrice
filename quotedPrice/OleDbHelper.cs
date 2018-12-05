//===============================================================================
// OleDbHelper based on Microsoft Data Access Application Block (DAAB) for .NET
// http://msdn.microsoft.com/library/en-us/dnbda/html/daab-rm.asp
//
// OleDbHelper.cs
//
// This file contains the implementations of the OleDbHelper and OleDbHelperParameterCache
// classes.
//
// The DAAB for MS Ole Db access for Oracle has been tested in the context of this Nile implementation,
// but has not undergone the generic functional testing that the SQL version has gone through.
// You can use it in other .NET applications using Oracle databases.  For complete docs explaining how to use
// and how it's built go to the originl appblock link. 
// For this sample, the code resides in the Nile namespaces not the Microsoft.ApplicationBlocks namespace
//==============================================================================

using System;
using System.Data;
using System.Xml;
using System.Data.OleDb;
using System.Collections;
using System.Windows.Forms;


namespace quotedPrice
{
	/// <summary>
	/// The OleDbHelper class is intended to encapsulate high performance, scalable best practices for 
	/// common uses of OleDbClient.
	/// </summary>
	public sealed class OleDbHelper
	{
        public static string ConnectionString;

        static OleDbHelper()
        {
            ConnectionString = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}\data\Gcys.mdb;Persist Security Info=True",
                Application.StartupPath);
        }

        #region private utility methods & constructors
        /// <summary>
        /// 批量事务执行SQL语句
        /// </summary>
        public static bool BatchExecuteNonQuery(OleDbConnection Conn, ArrayList ALSql)
        {
            if (Conn.State != ConnectionState.Open)
            {
                try
                {
                    Conn.Open();
                }
                catch
                {
                    throw new Exception("数据库无法连接");
                }
            }
            bool state = false;
            OleDbTransaction transaction = null;
            try
            {
                OleDbCommand cmd = new OleDbCommand();
                transaction = Conn.BeginTransaction();
                cmd.Transaction = transaction;
                cmd.Connection = Conn;
                cmd.CommandType = CommandType.Text;
                for (int i = 0; i < ALSql.Count; i++)
                {
                    cmd.CommandText = ALSql[i].ToString();
                    cmd.ExecuteNonQuery();
                }
                transaction.Commit();
                state = true;
            }
            catch
            {
                state = false;
                transaction.Rollback();
            }
            finally
            {
                Conn.Close();
            }
            return state;
        }
        //Since this class provides only static methods, make the default constructor private to prevent 
        //instances from being created with "new OleDbHelper()".
        private OleDbHelper() {}

		/// <summary>
		/// This method is used to attach array's of OleDbParameters to an OleDbCommand.
		/// 
		/// This method will assign a value of DbNull to any parameter with a direction of
		/// InputOutput and a value of null.  
		/// 
		/// This behavior will prevent default values from being used, but
		/// this will be the less common case than an intended pure output parameter (derived as InputOutput)
		/// where the user provided no input value.
		/// </summary>
		/// <param name="command">The command to which the parameters will be added</param>
		/// <param name="commandParameters">an array of OleDbParameters tho be added to command</param>
		private static void AttachParameters(OleDbCommand command, OleDbParameter[] commandParameters)
		{
			foreach (OleDbParameter p in commandParameters)
			{
				//check for derived output value with no value assigned
				if ((p.Direction == ParameterDirection.InputOutput) && (p.Value == null))
				{
					p.Value = DBNull.Value;
				}
				
				command.Parameters.Add(p);
			}
		}

		/// <summary>
		/// This method assigns an array of values to an array of OleDbParameters.
		/// </summary>
		/// <param name="commandParameters">array of OleDbParameters to be assigned values</param>
		/// <param name="parameterValues">array of objects holding the values to be assigned</param>
		private static void AssignParameterValues(OleDbParameter[] commandParameters, object[] parameterValues)
		{
			if ((commandParameters == null) || (parameterValues == null)) 
			{
				//do nothing if we get no data
				return;
			}

			// we must have the same number of values as we pave parameters to put them in
			if (commandParameters.Length != parameterValues.Length)
			{
				throw new ArgumentException("Parameter count does not match Parameter Value count.");
			}

			//iterate through the OleDbParameters, assigning the values from the corresponding position in the 
			//value array
			for (int i = 0, j = commandParameters.Length; i < j; i++)
			{
				commandParameters[i].Value = parameterValues[i];
			}
		}

		/// <summary>
		/// This method opens (if necessary) and assigns a connection, transaction, command type and parameters 
		/// to the provided command.
		/// </summary>
		/// <param name="command">the OleDbCommand to be prepared</param>
		/// <param name="connection">a valid OleDbConnection, on which to execute this command</param>
		/// <param name="transaction">a valid OleDbTransaction, or 'null'</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or PL/SQL command</param> 
		/// <param name="commandParameters">an array of OleDbParameters to be associated with the command or 'null' if no parameters are required</param>
		private static void PrepareCommand(OleDbCommand command, OleDbConnection connection, OleDbTransaction transaction, CommandType commandType, string commandText, OleDbParameter[] commandParameters)
		{
			//if the provided connection is not open, we will open it
			if (connection.State != ConnectionState.Open)
			{
				connection.Open();
			}

			//associate the connection with the command
			command.Connection = connection;

			//set the command text (stored procedure name or OleDb statement)
			command.CommandText = commandText;

			//if we were provided a transaction, assign it.
			if (transaction != null)
			{
				command.Transaction = transaction;
			}

			//set the command type
			command.CommandType = commandType;

			//attach the command parameters if they are provided
			if (commandParameters != null)
			{
				AttachParameters(command, commandParameters);
			}

			return;
		}


		#endregion private utility methods & constructors

		#region ExecuteNonQuery

		/// <summary>
		/// Execute an OleDbCommand (that returns no resultset and takes no parameters) against the database specified in 
		/// the connection string. 
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders");
		/// </remarks>
		/// <param name="connectionString">a valid connection string for an OleDbConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or PL/SQL command</param>  
		/// <returns>an int representing the number of rows affected by the command</returns>
		public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText)
		{
			//pass through the call providing null for the set of OleDbParameters
			return ExecuteNonQuery(connectionString, commandType, commandText, (OleDbParameter[])null);
		}

		/// <summary>
		/// Execute an OleDbCommand (that returns no resultset) against the database specified in the connection string 
		/// using the provided parameters.
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new OleDbParameter("@prodid", 24));
		/// </remarks>
		/// <param name="connectionString">a valid connection string for a OleDbConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or PL/SQL command</param>  
		/// <param name="commandParameters">an array of OleDbParameters used to execute the command</param>
		/// <returns>an int representing the number of rows affected by the command</returns>
		public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params OleDbParameter[] commandParameters)
		{
			//create & open an OleDbConnection, and dispose of it after we are done.
			using (OleDbConnection cn = new OleDbConnection(connectionString))
			{
				cn.Open();

				//call the overload that takes a connection in place of the connection string
				return ExecuteNonQuery(cn, commandType, commandText, commandParameters);
			}
		}

		/// <summary>
		/// Execute a stored procedure via an OleDbCommand (that returns no resultset) against the database specified in 
		/// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
		/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
		/// </summary>
		/// <remarks>
		/// This method provides no access to output parameters or the stored procedure's return value parameter.
		/// 
		/// e.g.:  
		///  int result = ExecuteNonQuery(connString, "PublishOrders", 24, 36);
		/// </remarks>
		/// <param name="connectionString">a valid connection string for a OleDbConnection</param>
		/// <param name="spName">the name of the stored prcedure</param>
		/// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
		/// <returns>an int representing the number of rows affected by the command</returns>
		public static int ExecuteNonQuery(string connectionString, string spName, params object[] parameterValues)
		{
			//if we got parameter values, we need to figure out where they go
			if ((parameterValues != null) && (parameterValues.Length > 0)) 
			{
				//pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
				OleDbParameter[] commandParameters = OleDbHelperParameterCache.GetSpParameterSet(connectionString, spName);

				//assign the provided values to these parameters based on parameter order
				AssignParameterValues(commandParameters, parameterValues);

				//call the overload that takes an array of OleDbParameters
				return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, commandParameters);
			}
			//otherwise we can just call the SP without params
			else 
			{
				return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName);
			}
		}

		/// <summary>
		/// Execute an OleDbDbCommand (that returns no resultset and takes no parameters) against the provided OleDbConnection. 
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  int result = ExecuteNonQuery(conn, CommandType.StoredProcedure, "PublishOrders");
		/// </remarks>
		/// <param name="connection">a valid OleDbConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or PL/SQL command</param>
		/// <returns>an int representing the number of rows affected by the command</returns>
		public static int ExecuteNonQuery(OleDbConnection connection, CommandType commandType, string commandText)
		{
			//pass through the call providing null for the set of OleDbParameters
			return ExecuteNonQuery(connection, commandType, commandText, (OleDbParameter[])null);
		}

		/// <summary>
		/// Execute an OleDbCommand (that returns no resultset) against the specified OleDbConnection 
		/// using the provided parameters.
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  int result = ExecuteNonQuery(conn, CommandType.StoredProcedure, "PublishOrders", new OleDbParameter("@prodid", 24));
		/// </remarks>
		/// <param name="connection">a valid OleDbConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or PL/SQL command</param>  
		/// <param name="commandParameters">an array of OleDbParamters used to execute the command</param>
		/// <returns>an int representing the number of rows affected by the command</returns>
		public static int ExecuteNonQuery(OleDbConnection connection, CommandType commandType, string commandText, params OleDbParameter[] commandParameters)
		{	
			//create a command and prepare it for execution
			OleDbCommand cmd = new OleDbCommand();
			PrepareCommand(cmd, connection, (OleDbTransaction)null, commandType, commandText, commandParameters);
			
			//finally, execute the command.
            try
            {
                return cmd.ExecuteNonQuery();
            }
            catch (Exception exp)
            {
                //MainForm.getInstance().toolStripStatusLabel.Text = exp.Message;
                MessageBox.Show(exp.Message, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;

            }

		}

		/// <summary>
		/// Execute a stored procedure via an OleDbCommand (that returns no resultset) against the specified OleDbConnection 
		/// using the provided parameter values.  This method will query the database to discover the parameters for the 
		/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
		/// </summary>
		/// <remarks>
		/// This method provides no access to output parameters or the stored procedure's return value parameter.
		/// 
		/// e.g.:  
		///  int result = ExecuteNonQuery(conn, "PublishOrders", 24, 36);
		/// </remarks>
		/// <param name="connection">a valid OleDbConnection</param>
		/// <param name="spName">the name of the stored prcedure</param>
		/// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
		/// <returns>an int representing the number of rows affected by the command</returns>
		public static int ExecuteNonQuery(OleDbConnection connection, string spName, params object[] parameterValues)
		{
			//if we got parameter values, we need to figure out where they go
			if ((parameterValues != null) && (parameterValues.Length > 0)) 
			{
				//pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
				OleDbParameter[] commandParameters = OleDbHelperParameterCache.GetSpParameterSet(connection.ConnectionString, spName);

				//assign the provided values to these parameters based on parameter order
				AssignParameterValues(commandParameters, parameterValues);

				//call the overload that takes an array of OleDbParameters
				return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName, commandParameters);
			}
			//otherwise we can just call the SP without params
			else 
			{
				return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName);
			}
		}

		/// <summary>
		/// Execute an OleDbCommand (that returns no resultset and takes no parameters) against the provided OleDbTransaction. 
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  int result = ExecuteNonQuery(trans, CommandType.StoredProcedure, "PublishOrders");
		/// </remarks>
		/// <param name="transaction">a valid OleDbTransaction</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or PL/SQL command</param>  
		/// <returns>an int representing the number of rows affected by the command</returns>
		public static int ExecuteNonQuery(OleDbTransaction transaction, CommandType commandType, string commandText)
		{
			//pass through the call providing null for the set of OleDbParameters
			return ExecuteNonQuery(transaction, commandType, commandText, (OleDbParameter[])null);
		}

		/// <summary>
		/// Execute an OleDbCommand (that returns no resultset) against the specified OleDbTransaction
		/// using the provided parameters.
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  int result = ExecuteNonQuery(trans, CommandType.StoredProcedure, "GetOrders", new OleDbParameter("@prodid", 24));
		/// </remarks>
		/// <param name="transaction">a valid OleDbTransaction</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or PL/SQL command</param>  
		/// <param name="commandParameters">an array of OleDbParamters used to execute the command</param>
		/// <returns>an int representing the number of rows affected by the command</returns>
		public static int ExecuteNonQuery(OleDbTransaction transaction, CommandType commandType, string commandText, params OleDbParameter[] commandParameters)
		{
			//create a command and prepare it for execution
			OleDbCommand cmd = new OleDbCommand();
			PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters);
			
			//finally, execute the command.
			return cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Execute a stored procedure via an OleDbCommand (that returns no resultset) against the specified 
		/// OleDbTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
		/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
		/// </summary>
		/// <remarks>
		/// This method provides no access to output parameters or the stored procedure's return value parameter.
		/// 
		/// e.g.:  
		///  int result = ExecuteNonQuery(conn, trans, "PublishOrders", 24, 36);
		/// </remarks>
		/// <param name="transaction">a valid OleDbTransaction</param>
		/// <param name="spName">the name of the stored procedure</param>
		/// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
		/// <returns>an int representing the number of rows affected by the command</returns>
		public static int ExecuteNonQuery(OleDbTransaction transaction, string spName, params object[] parameterValues)
		{
			//if we got parameter values, we need to figure out where they go
			if ((parameterValues != null) && (parameterValues.Length > 0)) 
			{
				//pull the parameters for this stored procedure from the parameter cache (or discover them & populet the cache)
				OleDbParameter[] commandParameters = OleDbHelperParameterCache.GetSpParameterSet(transaction.Connection.ConnectionString, spName);

				//assign the provided values to these parameters based on parameter order
				AssignParameterValues(commandParameters, parameterValues);

				//call the overload that takes an array of OleDbParameters
				return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, commandParameters);
			}
				//otherwise we can just call the SP without params
			else 
			{
				return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName);
			}
		}

		#endregion ExecuteNonQuery

		#region ExecuteDataSet

		/// <summary>
		/// Execute an OleDbCommand (that returns a resultset and takes no parameters) against the database specified in 
		/// the connection string. 
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  DataSet ds = ExecuteDataset(connString, CommandType.StoredProcedure, "GetOrders");
		/// </remarks>
		/// <param name="connectionString">a valid connection string for an OleDbConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or PL/SQL command</param>  
		/// <returns>a dataset containing the resultset generated by the command</returns>
		public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText)
		{
			//pass through the call providing null for the set of OleDbParameters
			return ExecuteDataset(connectionString, commandType, commandText, (OleDbParameter[])null);
		}

		/// <summary>
		/// Execute an OleDbCommand (that returns a resultset) against the database specified in the connection string 
		/// using the provided parameters.
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  DataSet ds = ExecuteDataset(connString, CommandType.StoredProcedure, "GetOrders", new OleDbParameter("@prodid", 24));
		/// </remarks>
		/// <param name="connectionString">a valid connection string for an OleDbConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or PL/SQL command</param> 
		/// <param name="commandParameters">an array of OleDbParamters used to execute the command</param>
		/// <returns>a dataset containing the resultset generated by the command</returns>
		public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText, params OleDbParameter[] commandParameters)
		{
			//create & open an OleDbConnection, and dispose of it after we are done.
			using (OleDbConnection cn = new OleDbConnection(connectionString))
			{
				cn.Open();

				//call the overload that takes a connection in place of the connection string
				return ExecuteDataset(cn, commandType, commandText, commandParameters);
			}
		}

		/// <summary>
		/// Execute a stored procedure via an OleDbCommand (that returns a resultset) against the database specified in 
		/// the conneciton string using the provided parameter values.  This method will query the database to discover the parameters for the 
		/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
		/// </summary>
		/// <remarks>
		/// This method provides no access to output parameters or the stored procedure's return value parameter.
		/// 
		/// e.g.:  
		///  DataSet ds = ExecuteDataset(connString, "GetOrders", 24, 36);
		/// </remarks>
		/// <param name="connectionString">a valid connection string for an OleDbConnection</param>
		/// <param name="spName">the name of the stored procedure</param>
		/// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
		/// <returns>a dataset containing the resultset generated by the command</returns>
		public static DataSet ExecuteDataset(string connectionString, string spName, params object[] parameterValues)
		{
			//if we got parameter values, we need to figure out where they go
			if ((parameterValues != null) && (parameterValues.Length > 0)) 
			{
				//pull the parameters for this stored procedure from the parameter cache (or discover them & populet the cache)
				OleDbParameter[] commandParameters = OleDbHelperParameterCache.GetSpParameterSet(connectionString, spName);

				//assign the provided values to these parameters based on parameter order
				AssignParameterValues(commandParameters, parameterValues);

				//call the overload that takes an array of OleDbParameters
				return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName, commandParameters);
			}
				//otherwise we can just call the SP without params
			else 
			{
				return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName);
			}
		}

		/// <summary>
		/// Execute an OleDbCommand (that returns a resultset and takes no parameters) against the provided OleDbConnection. 
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  DataSet ds = ExecuteDataset(conn, CommandType.StoredProcedure, "GetOrders");
		/// </remarks>
		/// <param name="connection">a valid OleDbConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or PL/SQL command</param>
		/// <returns>a dataset containing the resultset generated by the command</returns>
		public static DataSet ExecuteDataset(OleDbConnection connection, CommandType commandType, string commandText)
		{
			//pass through the call providing null for the set of OleDbParameters
			return ExecuteDataset(connection, commandType, commandText, (OleDbParameter[])null);
		}
		
		/// <summary>
		/// Execute an OleDbCommand (that returns a resultset) against the specified OleDbConnection 
		/// using the provided parameters.
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  DataSet ds = ExecuteDataset(conn, CommandType.StoredProcedure, "GetOrders", new OleDbParameter("@prodid", 24));
		/// </remarks>
		/// <param name="connection">a valid OleDbConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or PL/SQL command</param> 
		/// <param name="commandParameters">an array of OleDbParamters used to execute the command</param>
		/// <returns>a dataset containing the resultset generated by the command</returns>
		public static DataSet ExecuteDataset(OleDbConnection connection, CommandType commandType, string commandText, params OleDbParameter[] commandParameters)
		{
			//create a command and prepare it for execution
			OleDbCommand cmd = new OleDbCommand();
			PrepareCommand(cmd, connection, (OleDbTransaction)null, commandType, commandText, commandParameters);
			
			//create the DataAdapter & DataSet
			OleDbDataAdapter da = new OleDbDataAdapter(cmd);
			DataSet ds = new DataSet();

			//fill the DataSet using default values for DataTable names, etc.
			da.Fill(ds);
			
			//return the dataset
			return ds;						
		}
		
		/// <summary>
		/// Execute a stored procedure via an OleDbCommand (that returns a resultset) against the specified OleDbConnection 
		/// using the provided parameter values.  This method will query the database to discover the parameters for the 
		/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
		/// </summary>
		/// <remarks>
		/// This method provides no access to output parameters or the stored procedure's return value parameter.
		/// 
		/// e.g.:  
		///  DataSet ds = ExecuteDataset(conn, "GetOrders", 24, 36);
		/// </remarks>
		/// <param name="connection">a valid OleDbConnection</param>
		/// <param name="spName">the name of the stored prcedure</param>
		/// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
		/// <returns>a dataset containing the resultset generated by the command</returns>
		public static DataSet ExecuteDataset(OleDbConnection connection, string spName, params object[] parameterValues)
		{
			//if we got parameter values, we need to figure out where they go
			if ((parameterValues != null) && (parameterValues.Length > 0)) 
			{
				//pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
				OleDbParameter[] commandParameters = OleDbHelperParameterCache.GetSpParameterSet(connection.ConnectionString, spName);

				//assign the provided values to these parameters based on parameter order
				AssignParameterValues(commandParameters, parameterValues);

				//call the overload that takes an array of OleDbParameters
				return ExecuteDataset(connection, CommandType.StoredProcedure, spName, commandParameters);
			}
				//otherwise we can just call the SP without params
			else 
			{
				return ExecuteDataset(connection, CommandType.StoredProcedure, spName);
			}
		}

		/// <summary>
		/// Execute an OleDbCommand (that returns a resultset and takes no parameters) against the provided OleDbTransaction. 
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  DataSet ds = ExecuteDataset(trans, CommandType.StoredProcedure, "GetOrders");
		/// </remarks>
		/// <param name="transaction">a valid OleDbTransaction</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or PL/SQL command</param> 
		/// <returns>a dataset containing the resultset generated by the command</returns>
		public static DataSet ExecuteDataset(OleDbTransaction transaction, CommandType commandType, string commandText)
		{
			//pass through the call providing null for the set of OleDbParameters
			return ExecuteDataset(transaction, commandType, commandText, (OleDbParameter[])null);
		}
		
		/// <summary>
		/// Execute an OleDbCommand (that returns a resultset) against the specified OleDbTransaction
		/// using the provided parameters.
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  DataSet ds = ExecuteDataset(trans, CommandType.StoredProcedure, "GetOrders", new OleDbParameter("@prodid", 24));
		/// </remarks>
		/// <param name="transaction">a valid OleDbTransaction</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or PL/SQL command</param> 
		/// <param name="commandParameters">an array of OleDbParamters used to execute the command</param>
		/// <returns>a dataset containing the resultset generated by the command</returns>
		public static DataSet ExecuteDataset(OleDbTransaction transaction, CommandType commandType, string commandText, params OleDbParameter[] commandParameters)
		{
			//create a command and prepare it for execution
			OleDbCommand cmd = new OleDbCommand();
			PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters);
			
			//create the DataAdapter & DataSet
			OleDbDataAdapter da = new OleDbDataAdapter(cmd);
			DataSet ds = new DataSet();

			//fill the DataSet using default values for DataTable names, etc.
			da.Fill(ds);
			
			//return the dataset
			return ds;
		}
		
		/// <summary>
		/// Execute a stored procedure via an OleDbCommand (that returns a resultset) against the specified 
		/// OleDbTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
		/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
		/// </summary>
		/// <remarks>
		/// This method provides no access to output parameters or the stored procedure's return value parameter.
		/// 
		/// e.g.:  
		///  DataSet ds = ExecuteDataset(trans, "GetOrders", 24, 36);
		/// </remarks>
		/// <param name="transaction">a valid OleDbTransaction</param>
		/// <param name="spName">the name of the stored prcedure</param>
		/// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
		/// <returns>a dataset containing the resultset generated by the command</returns>
		public static DataSet ExecuteDataset(OleDbTransaction transaction, string spName, params object[] parameterValues)
		{
			//if we got parameter values, we need to figure out where they go
			if ((parameterValues != null) && (parameterValues.Length > 0)) 
			{
				//pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
				OleDbParameter[] commandParameters = OleDbHelperParameterCache.GetSpParameterSet(transaction.Connection.ConnectionString, spName);

				//assign the provided values to these parameters based on parameter order
				AssignParameterValues(commandParameters, parameterValues);

				//call the overload that takes an array of OleDbParameters
				return ExecuteDataset(transaction, CommandType.StoredProcedure, spName, commandParameters);
			}
				//otherwise we can just call the SP without params
			else 
			{
				return ExecuteDataset(transaction, CommandType.StoredProcedure, spName);
			}
		}

		#endregion ExecuteDataSet
		
		#region ExecuteReader

		/// <summary>
		/// this enum is used to indicate weather the connection was provided by the caller, or created by OleDbHelper, so that
		/// we can set the appropriate CommandBehavior when calling ExecuteReader()
		/// </summary>
		private enum OleDbConnectionOwnership	
		{
			/// <summary>Connection is owned and managed by OleDbHelper</summary>
			Internal, 
			/// <summary>Connection is owned and managed by the caller</summary>
			External
		}

		/// <summary>
		/// Create and prepare an OleDbCommand, and call ExecuteReader with the appropriate CommandBehavior.
		/// </summary>
		/// <remarks>
		/// If we created and opened the connection, we want the connection to be closed when the DataReader is closed.
		/// 
		/// If the caller provided the connection, we want to leave it to them to manage.
		/// </remarks>
		/// <param name="connection">a valid OleDbConnection, on which to execute this command</param>
		/// <param name="transaction">a valid OleDbTransaction, or 'null'</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or PL/SQL command</param> 
		/// <param name="commandParameters">an array of OleDbParameters to be associated with the command or 'null' if no parameters are required</param>
		/// <param name="connectionOwnership">indicates whether the connection parameter was provided by the caller, or created by OleDbHelper</param>
		/// <returns>OleDbDataReader containing the results of the command</returns>
		private static OleDbDataReader ExecuteReader(OleDbConnection connection, OleDbTransaction transaction, CommandType commandType, string commandText, OleDbParameter[] commandParameters, OleDbConnectionOwnership connectionOwnership)
		{	
			//create a command and prepare it for execution
			OleDbCommand cmd = new OleDbCommand();
			PrepareCommand(cmd, connection, transaction, commandType, commandText, commandParameters);
			
			//create a reader
			OleDbDataReader dr;

			// call ExecuteReader with the appropriate CommandBehavior
			if (connectionOwnership == OleDbConnectionOwnership.External)
			{
				dr = cmd.ExecuteReader();
			}
			else
			{
				dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
			}

			return dr;
		}

		/// <summary>
		/// Execute an OleDbCommand (that returns a resultset and takes no parameters) against the database specified in 
		/// the connection string. 
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  OleDbDataReader dr = ExecuteReader(connString, CommandType.StoredProcedure, "GetOrders");
		/// </remarks>
		/// <param name="connectionString">a valid connection string for an OleDbConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or PL/SQL command</param>  
		/// <returns>an OleDbDataReader containing the resultset generated by the command</returns>
		public static OleDbDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText)
		{
			//pass through the call providing null for the set of OleDbParameters
			return ExecuteReader(connectionString, commandType, commandText, (OleDbParameter[])null);
		}

		/// <summary>
		/// Execute an OleDbCommand (that returns a resultset) against the database specified in the connection string 
		/// using the provided parameters.
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  OleDbDataReader dr = ExecuteReader(connString, CommandType.StoredProcedure, "GetOrders", new OleDbParameter("@prodid", 24));
		/// </remarks>
		/// <param name="connectionString">a valid connection string for an OleDbConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or PL/SQL command</param>  
		/// <param name="commandParameters">an array of OleDbParameters used to execute the command</param>
		/// <returns>an OleDbDataReader containing the resultset generated by the command</returns>
		public static OleDbDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, params OleDbParameter[] commandParameters)
		{
			//create & open an OleDbbConnection
			OleDbConnection cn = new OleDbConnection(connectionString);
			cn.Open();

			try
			{
				//call the private overload that takes an internally owned connection in place of the connection string
				return ExecuteReader(cn, null, commandType, commandText, commandParameters, OleDbConnectionOwnership.Internal);
			}
			catch
			{
				//if we fail to return the OleDbDataReader, we neeed to close the connection ourselves
				cn.Close();
				throw;
			}
		}

		/// <summary>
		/// Execute a stored procedure via an OleDbCommand (that returns a resultset) against the database specified in 
		/// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
		/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
		/// </summary>
		/// <remarks>
		/// This method provides no access to output parameters or the stored procedure's return value parameter.
		/// 
		/// e.g.:  
		///  OleDbDataReader dr = ExecuteReader(connString, "GetOrders", 24, 36);
		/// </remarks>
		/// <param name="connectionString">a valid connection string for an OleDbConnection</param>
		/// <param name="spName">the name of the stored prcedure</param>
		/// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
		/// <returns>an OleDbDataReader containing the resultset generated by the command</returns>
		public static OleDbDataReader ExecuteReader(string connectionString, string spName, params object[] parameterValues)
		{
			//if we got parameter values, we need to figure out where they go
			if ((parameterValues != null) && (parameterValues.Length > 0)) 
			{
				//pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
				OleDbParameter[] commandParameters = OleDbHelperParameterCache.GetSpParameterSet(connectionString, spName);

				//assign the provided values to these parameters based on parameter order
				AssignParameterValues(commandParameters, parameterValues);

				//call the overload that takes an array of OleDbParameters
				return ExecuteReader(connectionString, CommandType.StoredProcedure, spName, commandParameters);
			}
			//otherwise we can just call the SP without params
			else 
			{
				return ExecuteReader(connectionString, CommandType.StoredProcedure, spName);
			}
		}

		/// <summary>
		/// Execute an OleDbCommand (that returns a resultset and takes no parameters) against the provided OleDbConnection. 
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  OleDbDataReader dr = ExecuteReader(conn, CommandType.StoredProcedure, "GetOrders");
		/// </remarks>
		/// <param name="connection">a valid OleDbConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or PL/SQL command</param>
		/// <returns>an OleDbDataReader containing the resultset generated by the command</returns>
		public static OleDbDataReader ExecuteReader(OleDbConnection connection, CommandType commandType, string commandText)
		{
			//pass through the call providing null for the set of OleDbParameters
			return ExecuteReader(connection, commandType, commandText, (OleDbParameter[])null);
		}

		/// <summary>
		/// Execute an OleDbCommand (that returns a resultset) against the specified OleDbConnection 
		/// using the provided parameters.
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  OleDbDataReader dr = ExecuteReader(conn, CommandType.StoredProcedure, "GetOrders", new OleDbParameter("@prodid", 24));
		/// </remarks>
		/// <param name="connection">a valid OleDbConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or PL/SQL command</param>  
		/// <param name="commandParameters">an array of OleDbParamters used to execute the command</param>
		/// <returns>an OleDbDataReader containing the resultset generated by the command</returns>
		public static OleDbDataReader ExecuteReader(OleDbConnection connection, CommandType commandType, string commandText, params OleDbParameter[] commandParameters)
		{
			//pass through the call to the private overload using a null transaction value and an externally owned connection
			return ExecuteReader(connection, (OleDbTransaction)null, commandType, commandText, commandParameters, OleDbConnectionOwnership.External);
		}

		/// <summary>
		/// Execute a stored procedure via an OleDbCommand (that returns a resultset) against the specified OleDbConnection 
		/// using the provided parameter values.  This method will query the database to discover the parameters for the 
		/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
		/// </summary>
		/// <remarks>
		/// This method provides no access to output parameters or the stored procedure's return value parameter.
		/// 
		/// e.g.:  
		///  OleDbDataReader dr = ExecuteReader(conn, "GetOrders", 24, 36);
		/// </remarks>
		/// <param name="connection">a valid OleDbConnection</param>
		/// <param name="spName">the name of the stored procedure</param>
		/// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
		/// <returns>an OleDbDataReader containing the resultset generated by the command</returns>
		public static OleDbDataReader ExecuteReader(OleDbConnection connection, string spName, params object[] parameterValues)
		{
			//if we got parameter values, we need to figure out where they go
			if ((parameterValues != null) && (parameterValues.Length > 0)) 
			{
				OleDbParameter[] commandParameters = OleDbHelperParameterCache.GetSpParameterSet(connection.ConnectionString, spName);

				AssignParameterValues(commandParameters, parameterValues);

				return ExecuteReader(connection, CommandType.StoredProcedure, spName, commandParameters);
			}
			//otherwise we can just call the SP without params
			else 
			{
				return ExecuteReader(connection, CommandType.StoredProcedure, spName);
			}
		}

		/// <summary>
		/// Execute an OleDbCommand (that returns a resultset and takes no parameters) against the provided OleDbTransaction. 
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  OleDbDataReader dr = ExecuteReader(trans, CommandType.StoredProcedure, "GetOrders");
		/// </remarks>
		/// <param name="transaction">a valid OleDbTransaction</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or PL/SQL command</param>  
		/// <returns>an OleDbDataReader containing the resultset generated by the command</returns>
		public static OleDbDataReader ExecuteReader(OleDbTransaction transaction, CommandType commandType, string commandText)
		{
			//pass through the call providing null for the set of OleDbParameters
			return ExecuteReader(transaction, commandType, commandText, (OleDbParameter[])null);
		}

		/// <summary>
		/// Execute an OleDbCommand (that returns a resultset) against the specified OleDbTransaction
		/// using the provided parameters.
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///   OleDbDataReader dr = ExecuteReader(trans, CommandType.StoredProcedure, "GetOrders", new OleDbParameter("@prodid", 24));
		/// </remarks>
		/// <param name="transaction">a valid OleDbTransaction</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or PL/SQL command</param> 
		/// <param name="commandParameters">an array of OleDbParameters used to execute the command</param>
		/// <returns>an OleDbDataReader containing the resultset generated by the command</returns>
		public static OleDbDataReader ExecuteReader(OleDbTransaction transaction, CommandType commandType, string commandText, params OleDbParameter[] commandParameters)
		{
			//pass through to private overload, indicating that the connection is owned by the caller
			return ExecuteReader(transaction.Connection, transaction, commandType, commandText, commandParameters, OleDbConnectionOwnership.External);
		}

		/// <summary>
		/// Execute a stored procedure via an OleDbCommand (that returns a resultset) against the specified
		/// OleDbTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
		/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
		/// </summary>
		/// <remarks>
		/// This method provides no access to output parameters or the stored procedure's return value parameter.
		/// 
		/// e.g.:  
		///  OleDbDataReader dr = ExecuteReader(trans, "GetOrders", 24, 36);
		/// </remarks>
		/// <param name="transaction">a valid OleDbTransaction</param>
		/// <param name="spName">the name of the stored prcedure</param>
		/// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
		/// <returns>an OleDbDataReader containing the resultset generated by the command</returns>
		public static OleDbDataReader ExecuteReader(OleDbTransaction transaction, string spName, params object[] parameterValues)
		{
			//if we got parameter values, we need to figure out where they go
			if ((parameterValues != null) && (parameterValues.Length > 0)) 
			{
				OleDbParameter[] commandParameters = OleDbHelperParameterCache.GetSpParameterSet(transaction.Connection.ConnectionString, spName);

				AssignParameterValues(commandParameters, parameterValues);

				return ExecuteReader(transaction, CommandType.StoredProcedure, spName, commandParameters);
			}
				//otherwise we can just call the SP without params
			else 
			{
				return ExecuteReader(transaction, CommandType.StoredProcedure, spName);
			}
		}

		#endregion ExecuteReader

		#region ExecuteScalar
		
		/// <summary>
		/// Execute a OleDbCommand (that returns a 1x1 resultset and takes no parameters) against the database specified in 
		/// the connection string. 
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  int orderCount = (int)ExecuteScalar(connString, CommandType.StoredProcedure, "GetOrderCount");
		/// </remarks>
		/// <param name="connectionString">a valid connection string for a OleDbConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-OleDb command</param>
		/// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
		public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText)
		{
			//pass through the call providing null for the set of OleDbParameters
			return ExecuteScalar(connectionString, commandType, commandText, (OleDbParameter[])null);
		}

		/// <summary>
		/// Execute a OleDbCommand (that returns a 1x1 resultset) against the database specified in the connection string 
		/// using the provided parameters.
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  int orderCount = (int)ExecuteScalar(connString, CommandType.StoredProcedure, "GetOrderCount", new OleDbParameter("@prodid", 24));
		/// </remarks>
		/// <param name="connectionString">a valid connection string for a OleDbConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-OleDb command</param>
		/// <param name="commandParameters">an array of OleDbParamters used to execute the command</param>
		/// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
		public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText, params OleDbParameter[] commandParameters)
		{
			//create & open a OleDbConnection, and dispose of it after we are done.
			using (OleDbConnection cn = new OleDbConnection(connectionString))
			{
				cn.Open();

				//call the overload that takes a connection in place of the connection string
				return ExecuteScalar(cn, commandType, commandText, commandParameters);
			}
		}

		/// <summary>
		/// Execute a stored procedure via a OleDbCommand (that returns a 1x1 resultset) against the database specified in 
		/// the conneciton string using the provided parameter values.  This method will query the database to discover the parameters for the 
		/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
		/// </summary>
		/// <remarks>
		/// This method provides no access to output parameters or the stored procedure's return value parameter.
		/// 
		/// e.g.:  
		///  int orderCount = (int)ExecuteScalar(connString, "GetOrderCount", 24, 36);
		/// </remarks>
		/// <param name="connectionString">a valid connection string for a OleDbConnection</param>
		/// <param name="spName">the name of the stored prcedure</param>
		/// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
		/// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
		public static object ExecuteScalar(string connectionString, string spName, params object[] parameterValues)
		{
			//if we got parameter values, we need to figure out where they go
			if ((parameterValues != null) && (parameterValues.Length > 0)) 
			{
				//pull the parameters for this stored procedure from the parameter cache (or discover them & populet the cache)
				OleDbParameter[] commandParameters = OleDbHelperParameterCache.GetSpParameterSet(connectionString, spName);

				//assign the provided values to these parameters based on parameter order
				AssignParameterValues(commandParameters, parameterValues);

				//call the overload that takes an array of OleDbParameters
				return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName, commandParameters);
			}
				//otherwise we can just call the SP without params
			else 
			{
				return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName);
			}
		}

		/// <summary>
		/// Execute a OleDbCommand (that returns a 1x1 resultset and takes no parameters) against the provided OleDbConnection. 
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  int orderCount = (int)ExecuteScalar(conn, CommandType.StoredProcedure, "GetOrderCount");
		/// </remarks>
		/// <param name="connection">a valid OleDbConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-OleDb command</param>
		/// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
		public static object ExecuteScalar(OleDbConnection connection, CommandType commandType, string commandText)
		{
			//pass through the call providing null for the set of OleDbParameters
			return ExecuteScalar(connection, commandType, commandText, (OleDbParameter[])null);
		}

		/// <summary>
		/// Execute a OleDbCommand (that returns a 1x1 resultset) against the specified OleDbConnection 
		/// using the provided parameters.
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  int orderCount = (int)ExecuteScalar(conn, CommandType.StoredProcedure, "GetOrderCount", new OleDbParameter("@prodid", 24));
		/// </remarks>
		/// <param name="connection">a valid OleDbConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-OleDb command</param>
		/// <param name="commandParameters">an array of OleDbParamters used to execute the command</param>
		/// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
		public static object ExecuteScalar(OleDbConnection connection, CommandType commandType, string commandText, params OleDbParameter[] commandParameters)
		{
			//create a command and prepare it for execution
			OleDbCommand cmd = new OleDbCommand();
			PrepareCommand(cmd, connection, (OleDbTransaction)null, commandType, commandText, commandParameters);
			
			//execute the command & return the results
			return cmd.ExecuteScalar();
		}

		/// <summary>
		/// Execute a stored procedure via a OleDbCommand (that returns a 1x1 resultset) against the specified OleDbConnection 
		/// using the provided parameter values.  This method will query the database to discover the parameters for the 
		/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
		/// </summary>
		/// <remarks>
		/// This method provides no access to output parameters or the stored procedure's return value parameter.
		/// 
		/// e.g.:  
		///  int orderCount = (int)ExecuteScalar(conn, "GetOrderCount", 24, 36);
		/// </remarks>
		/// <param name="connection">a valid OleDbConnection</param>
		/// <param name="spName">the name of the stored prcedure</param>
		/// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
		/// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
		public static object ExecuteScalar(OleDbConnection connection, string spName, params object[] parameterValues)
		{
			//if we got parameter values, we need to figure out where they go
			if ((parameterValues != null) && (parameterValues.Length > 0)) 
			{
				//pull the parameters for this stored procedure from the parameter cache (or discover them & populet the cache)
				OleDbParameter[] commandParameters = OleDbHelperParameterCache.GetSpParameterSet(connection.ConnectionString, spName);

				//assign the provided values to these parameters based on parameter order
				AssignParameterValues(commandParameters, parameterValues);

				//call the overload that takes an array of OleDbParameters
				return ExecuteScalar(connection, CommandType.StoredProcedure, spName, commandParameters);
			}
				//otherwise we can just call the SP without params
			else 
			{
				return ExecuteScalar(connection, CommandType.StoredProcedure, spName);
			}
		}

		/// <summary>
		/// Execute a OleDbCommand (that returns a 1x1 resultset and takes no parameters) against the provided OleDbTransaction. 
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  int orderCount = (int)ExecuteScalar(trans, CommandType.StoredProcedure, "GetOrderCount");
		/// </remarks>
		/// <param name="transaction">a valid OleDbTransaction</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-OleDb command</param>
		/// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
		public static object ExecuteScalar(OleDbTransaction transaction, CommandType commandType, string commandText)
		{
			//pass through the call providing null for the set of OleDbParameters
			return ExecuteScalar(transaction, commandType, commandText, (OleDbParameter[])null);
		}

		/// <summary>
		/// Execute a OleDbCommand (that returns a 1x1 resultset) against the specified OleDbTransaction
		/// using the provided parameters.
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  int orderCount = (int)ExecuteScalar(trans, CommandType.StoredProcedure, "GetOrderCount", new OleDbParameter("@prodid", 24));
		/// </remarks>
		/// <param name="transaction">a valid OleDbTransaction</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-OleDb command</param>
		/// <param name="commandParameters">an array of OleDbParamters used to execute the command</param>
		/// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
		public static object ExecuteScalar(OleDbTransaction transaction, CommandType commandType, string commandText, params OleDbParameter[] commandParameters)
		{
			//create a command and prepare it for execution
			OleDbCommand cmd = new OleDbCommand();
			PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters);
			
			//execute the command & return the results
			return cmd.ExecuteScalar();

		}

		/// <summary>
		/// Execute a stored procedure via a OleDbCommand (that returns a 1x1 resultset) against the specified
		/// OleDbTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
		/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
		/// </summary>
		/// <remarks>
		/// This method provides no access to output parameters or the stored procedure's return value parameter.
		/// 
		/// e.g.:  
		///  int orderCount = (int)ExecuteScalar(trans, "GetOrderCount", 24, 36);
		/// </remarks>
		/// <param name="transaction">a valid OleDbTransaction</param>
		/// <param name="spName">the name of the stored prcedure</param>
		/// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
		/// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
		public static object ExecuteScalar(OleDbTransaction transaction, string spName, params object[] parameterValues)
		{
			//if we got parameter values, we need to figure out where they go
			if ((parameterValues != null) && (parameterValues.Length > 0)) 
			{
				//pull the parameters for this stored procedure from the parameter cache (or discover them & populet the cache)
				OleDbParameter[] commandParameters = OleDbHelperParameterCache.GetSpParameterSet(transaction.Connection.ConnectionString, spName);

				//assign the provided values to these parameters based on parameter order
				AssignParameterValues(commandParameters, parameterValues);

				//call the overload that takes an array of OleDbParameters
				return ExecuteScalar(transaction, CommandType.StoredProcedure, spName, commandParameters);
			}
				//otherwise we can just call the SP without params
			else 
			{
				return ExecuteScalar(transaction, CommandType.StoredProcedure, spName);
			}
		}

		#endregion ExecuteScalar


        public static DataTable GetExcelTables(string connString)
        {
            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                conn.Open();
                DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                conn.Close();
                return schemaTable;
            }
        }
        public static DataTable FillDataTable(string connString, CommandType cmdType, string cmdText, params OleDbParameter[] cmdParms)
        {
            OleDbCommand cmd = new OleDbCommand();
            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                DataTable dt = new DataTable();
                PrepareCommand(cmd, conn, (OleDbTransaction)null, cmdType, cmdText, cmdParms);
                OleDbDataAdapter sda = new OleDbDataAdapter(cmd);
                sda.Fill(dt);
                cmd.Parameters.Clear();
                sda.Dispose();
                return dt;
            }
        }

        public static DataTable FillDataTable(string connString, string cmdText, params OleDbParameter[] cmdParms)
        {
            return FillDataTable(connString, CommandType.Text, cmdText, cmdParms);
        }

        public static DataTable GetMDBTables(string connString)
        {
            DataTable schemaTable = FillDataTable(connString, "SELECT [NAME] AS TABLE_NAME FROM MSYSOBJECTS WHERE FLAGS=0 AND [TYPE]=1");
            return schemaTable;
        }

        /// <summary> 
        /// 从Excel读取数据 
        /// </summary> 
        /// <param name="filePath">路径</param> 
        /// <returns>DataSet</returns> 
        public static DataSet ImportFromExcel(string filePath)
        {
            DataSet ds = new DataSet();
            string connString = "Provider = Microsoft.Jet.OLEDB.4.0 ; Data Source = " + filePath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
            DataTable table = GetExcelTables(connString);
            if (table == null || table.Rows.Count <= 0)
            {
                return null;
            }

            foreach (DataRow dr in table.Rows)
            {
                string cmdText = "select * from [" + dr["TABLE_NAME"].ToString() + "]";
                DataTable dt = FillDataTable(connString, cmdText);
                dt.TableName = dr["TABLE_NAME"].ToString();
                ds.Tables.Add(dt);
            }

            return ds;
        }

        /// <summary>
        /// 将数据导出至Excel
        /// </summary>
        /// <param name="Table">DataTable对象</param>
        /// <param name="ExcelFilePath">Excel文件路径</param>
        /// <returns></returns>
        public static bool OutputToExcel(DataSet source, string ExcelFilePath)
        {
            //连接字符串
            string connString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ExcelFilePath + ";Extended Properties=Excel 8.0;";
            OleDbConnection objConn = new OleDbConnection(connString);

            for (int x = 0; x < source.Tables.Count; x++)
            {
                //创建表结构
                OleDbCommand objCmd = new OleDbCommand();

                DataTable Table = source.Tables[x];
                if ((Table.TableName.Trim().Length == 0) || (Table.TableName.ToLower() == "table"))
                {
                    Table.TableName = "Sheet"+(x+1);
                }
                //数据表的列数
                int ColCount = Table.Columns.Count;
                //用于记数，实例化参数时的序号
                int i = 0;
                //创建参数
                OleDbParameter[] para = new OleDbParameter[ColCount];
                //创建表结构的SQL语句
                string DropTableStr = @"drop Table " + Table.TableName;
                string TableStructStr = @"Create Table " + Table.TableName + "(";

                //遍历数据表的所有列，用于创建表结构
                foreach (DataColumn col in Table.Columns)
                {
                    para[i] = new OleDbParameter("@" + col.ColumnName, OleDbType.VarChar);
                    objCmd.Parameters.Add(para[i]);
                    //如果是最后一列
                    if (i + 1 == ColCount)
                    {
                        TableStructStr += col.ColumnName + " varchar)";
                    }
                    else
                    {
                        TableStructStr += col.ColumnName + " varchar,";
                    }
                    i++;
                }
                //创建Excel文件及文件结构
                try
                {
                    objCmd.Connection = objConn;

                    if (objConn.State == ConnectionState.Closed)
                    {
                        objConn.Open();
                    }
                    //新建
                    objCmd.CommandText = TableStructStr;
                    objCmd.ExecuteNonQuery();
               }
                catch (Exception exp)
                {
                    //throw exp;
                }
                //插入记录的SQL语句
                string InsertSql_1 = "Insert into " + Table.TableName + " (";
                string InsertSql_2 = " Values (";
                string InsertSql = "";
                //遍历所有列，用于插入记录，在此创建插入记录的SQL语句
                for (int colID = 0; colID < ColCount; colID++)
                {
                    if (colID + 1 == ColCount)  //最后一列
                    {
                        InsertSql_1 += Table.Columns[colID].ColumnName + ")";
                        InsertSql_2 += "@" + Table.Columns[colID].ColumnName + ")";
                    }
                    else
                    {
                        InsertSql_1 += Table.Columns[colID].ColumnName + ",";
                        InsertSql_2 += "@" + Table.Columns[colID].ColumnName + ",";
                    }
                }

                InsertSql = InsertSql_1 + InsertSql_2;

                //遍历数据表的所有数据行
                for (int rowID = 0; rowID < Table.Rows.Count; rowID++)
                {
                    for (int colID = 0; colID < ColCount; colID++)
                    {
                        object value = Table.Rows[rowID][colID];
                        if (value == null)
                            para[colID].Value = "";
                        else
                            para[colID].Value = value.ToString().Trim();
                    }

                    try
                    {
                        objCmd.CommandText = InsertSql;
                        objCmd.ExecuteNonQuery();
                    }
                    catch (Exception exp)
                    {
                        throw exp;
                    }
                }

                try
                {
                    if (objConn.State == ConnectionState.Open)
                    {
                        objConn.Close();
                    }
                }
                catch (Exception exp)
                {
                    throw exp;
                }

            }
            return true;
        }

        /// <summary>
        /// 将数据导出至Excel
        /// </summary>
        /// <param name="Table">DataTable对象</param>
        /// <param name="ExcelFilePath">Excel文件路径</param>
        /// <returns></returns>
        public static bool OutputToExcelWithColumnType(DataSet source, string ExcelFilePath)
        {
            //连接字符串
            string connString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ExcelFilePath + ";Extended Properties=Excel 8.0;";
            OleDbConnection objConn = new OleDbConnection(connString);
            //数据类型集合
            ArrayList DataTypeList = new ArrayList();
            DataTypeList.Add("System.Decimal");
            DataTypeList.Add("System.Double");
            DataTypeList.Add("System.Int16");
            DataTypeList.Add("System.Int32");
            DataTypeList.Add("System.Int64");
            DataTypeList.Add("System.Single");
            for (int x = 0; x < source.Tables.Count; x++)
            {
                //创建表结构
                OleDbCommand objCmd = new OleDbCommand();

                DataTable Table = source.Tables[x];
                if ((Table.TableName.Trim().Length == 0) || (Table.TableName.ToLower() == "table"))
                {
                    Table.TableName = "Sheet" + (x + 1);
                }
                //数据表的列数
                int ColCount = Table.Columns.Count;
                //用于记数，实例化参数时的序号
                int i = 0;
                //创建参数
                OleDbParameter[] para = new OleDbParameter[ColCount];
                //创建表结构的SQL语句
                string DropTableStr = @"drop Table " + Table.TableName;
                string TableStructStr = @"Create Table " + Table.TableName + "(";

                //遍历数据表的所有列，用于创建表结构
                foreach (DataColumn col in Table.Columns)
                {
                    //如果列属于数字列，则设置该列的数据类型为double
                    if (DataTypeList.IndexOf(col.DataType.ToString()) >= 0)
                    {
                        para[i] = new OleDbParameter("@" + col.ColumnName, OleDbType.Double);
                        objCmd.Parameters.Add(para[i]);
                        //如果是最后一列
                        if (i + 1 == ColCount)
                        {
                            TableStructStr += col.ColumnName + " double)";
                        }
                        else
                        {
                            TableStructStr += col.ColumnName + " double,";
                        }
                    }
                    else
                    {
                        para[i] = new OleDbParameter("@" + col.ColumnName, OleDbType.VarChar);
                        objCmd.Parameters.Add(para[i]);
                        //如果是最后一列
                        if (i + 1 == ColCount)
                        {
                            TableStructStr += col.ColumnName + " varchar)";
                        }
                        else
                        {
                            TableStructStr += col.ColumnName + " varchar,";
                        }
                    }

                    i++;
                }
                //创建Excel文件及文件结构
                try
                {
                    objCmd.Connection = objConn;

                    if (objConn.State == ConnectionState.Closed)
                    {
                        objConn.Open();
                    }
                    //新建
                    objCmd.CommandText = TableStructStr;
                    objCmd.ExecuteNonQuery();
                }
                catch (Exception exp)
                {
                    //throw exp;
                }
                //插入记录的SQL语句
                string InsertSql_1 = "Insert into " + Table.TableName + " (";
                string InsertSql_2 = " Values (";
                string InsertSql = "";
                //遍历所有列，用于插入记录，在此创建插入记录的SQL语句
                for (int colID = 0; colID < ColCount; colID++)
                {
                    if (colID + 1 == ColCount)  //最后一列
                    {
                        InsertSql_1 += Table.Columns[colID].ColumnName + ")";
                        InsertSql_2 += "@" + Table.Columns[colID].ColumnName + ")";
                    }
                    else
                    {
                        InsertSql_1 += Table.Columns[colID].ColumnName + ",";
                        InsertSql_2 += "@" + Table.Columns[colID].ColumnName + ",";
                    }
                }

                InsertSql = InsertSql_1 + InsertSql_2;

                //遍历数据表的所有数据行
                for (int rowID = 0; rowID < Table.Rows.Count; rowID++)
                {
                    for (int colID = 0; colID < ColCount; colID++)
                    {
                        if (para[colID].DbType == DbType.Double && Table.Rows[rowID][colID].ToString().Trim() == "")
                        {
                            para[colID].Value = 0;
                        }
                        else
                        {
                            para[colID].Value = Table.Rows[rowID][colID].ToString().Trim();
                        }
                    }

                    try
                    {
                        objCmd.CommandText = InsertSql;
                        objCmd.ExecuteNonQuery();
                    }
                    catch (Exception exp)
                    {
                        throw exp;
                    }
                }

                try
                {
                    if (objConn.State == ConnectionState.Open)
                    {
                        objConn.Close();
                    }
                }
                catch (Exception exp)
                {
                    throw exp;
                }

            }
            return true;
        }
        /**/
        /// <summary>
        /// 获取当前页应该显示的记录，注意：查询中必须包含名为ID的自动编号列，若不符合你的要求，就修改一下源码吧 :)
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">分页容量</param>
        /// <param name="showString">显示的字段</param>
        /// <param name="queryString">查询字符串，支持联合查询</param>
        /// <param name="whereString">查询条件，若有条件限制则必须以where 开头</param>
        /// <param name="orderString">排序规则</param>
        /// <param name="pageCount">传出参数：总页数统计</param>
        /// <param name="recordCount">传出参数：总记录统计</param>
        /// <returns>装载记录的OleDbDataReader</returns>
        public static OleDbDataReader ExecutePager(string connectionString, CommandType commandType, int pageIndex, int pageSize, string showString, string queryString, string whereString, string orderString, ref int pageCount, ref int recordCount)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;
            if (string.IsNullOrEmpty(showString)) showString = "*";
            if (string.IsNullOrEmpty(orderString)) orderString = "ID desc";


            OleDbConnection connection = new OleDbConnection(connectionString);
            connection.Open();
            string myVw = string.Format(" ( {0} ) tempVw ", queryString);
            string commandText = string.Format(" select count(*) as recordCount from {0} {1}", myVw, whereString);
            recordCount = Convert.ToInt32(ExecuteScalar(connection, commandType, commandText));

            if ((recordCount % pageSize) > 0)
                pageCount = recordCount / pageSize + 1;
            else
                pageCount = recordCount / pageSize;
            OleDbCommand cmdRecord;
            if (pageIndex == 1)//第一页
            {
                cmdRecord = new OleDbCommand(string.Format("select top {0} {1} from {2} {3} order by {4} ", pageSize, showString, myVw, whereString, orderString), connection);
            }
            else if (pageIndex > pageCount)//超出总页数
            {
                cmdRecord = new OleDbCommand(string.Format("select top {0} {1} from {2} {3} order by {4} ", pageSize, showString, myVw, "where 1=2", orderString), connection);
            }
            else
            {
                int pageLowerBound = pageSize * pageIndex;
                int pageUpperBound = pageLowerBound - pageSize;
                string recordIDs = recordID(string.Format("select top {0} {1} from {2} {3} order by {4} ", pageLowerBound, "ID", myVw, whereString, orderString), pageUpperBound, connection);
                cmdRecord = new OleDbCommand(string.Format("select {0} from {1} where id in ({2}) order by {3} ", showString, myVw, recordIDs, orderString), connection);

            }
            return cmdRecord.ExecuteReader();
        }
        private static string recordID(string query, int passCount, OleDbConnection m_Conn)
        {
            OleDbCommand cmd = new OleDbCommand(query, m_Conn);
            string result = string.Empty;
            using (IDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    if (passCount < 1)
                    {
                        result += "," + dr.GetDecimal(0);
                    }
                    passCount--;
                }
            }
            return result.Substring(1);
        }

	}

	/// <summary>
	/// OleDbHelperParameterCache provides functions to leverage a static cache of procedure parameters, and the
	/// ability to discover parameters for stored procedures at run-time.
	/// </summary>
    public sealed class OleDbHelperParameterCache
    {
        #region private methods, variables, and constructors

        //Since this class provides only static methods, make the default constructor private to prevent 
        //instances from being created with "new OleDbHelperParameterCache()".
        private OleDbHelperParameterCache() { }

        private static Hashtable paramCache = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// resolve at run-time the appropriate set of OleDbParameters for a stored procedure
        /// </summary>
        /// <param name="connectionString">a valid connection string for a OleDbConnection</param>
        /// <param name="spName">the name of the stored prcedure</param>
        /// <param name="includeReturnValueParameter">weather or not to onclude ther return value parameter</param>
        /// <returns></returns>
        private static OleDbParameter[] DiscoverSpParameterSet(string connectionString, string spName, bool includeReturnValueParameter)
        {
            using (OleDbConnection cn = new OleDbConnection(connectionString))
            using (OleDbCommand cmd = new OleDbCommand(spName, cn))
            {
                cn.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                OleDbCommandBuilder.DeriveParameters(cmd);

                if (!includeReturnValueParameter)
                {
                    if (ParameterDirection.ReturnValue == cmd.Parameters[0].Direction)
                        cmd.Parameters.RemoveAt(0);
                }

                OleDbParameter[] discoveredParameters = new OleDbParameter[cmd.Parameters.Count];

                cmd.Parameters.CopyTo(discoveredParameters, 0);

                return discoveredParameters;
            }
        }

        //deep copy of cached OleDbParameter array
        private static OleDbParameter[] CloneParameters(OleDbParameter[] originalParameters)
        {
            OleDbParameter[] clonedParameters = new OleDbParameter[originalParameters.Length];

            for (int i = 0, j = originalParameters.Length; i < j; i++)
            {
                clonedParameters[i] = (OleDbParameter)((ICloneable)originalParameters[i]).Clone();
            }

            return clonedParameters;
        }

        #endregion private methods, variables, and constructors

        #region caching functions

        /// <summary>
        /// add parameter array to the cache
        /// </summary>
        /// <param name="connectionString">a valid connection string for an OleDbConnection</param>
        /// <param name="commandText">the stored procedure name or T-OleDb command</param>
        /// <param name="commandParameters">an array of OleDbParamters to be cached</param>
        public static void CacheParameterSet(string connectionString, string commandText, params OleDbParameter[] commandParameters)
        {
            string hashKey = connectionString + ":" + commandText;

            paramCache[hashKey] = commandParameters;
        }

        /// <summary>
        /// retrieve a parameter array from the cache
        /// </summary>
        /// <param name="connectionString">a valid connection string for a OleDbConnection</param>
        /// <param name="commandText">the stored procedure name or T-OleDb command</param>
        /// <returns>an array of OleDbParameters</returns>
        public static OleDbParameter[] GetCachedParameterSet(string connectionString, string commandText)
        {
            string hashKey = connectionString + ":" + commandText;

            OleDbParameter[] cachedParameters = (OleDbParameter[])paramCache[hashKey];

            if (cachedParameters == null)
            {
                return null;
            }
            else
            {
                return CloneParameters(cachedParameters);
            }
        }

        #endregion caching functions

        #region Parameter Discovery Functions

        /// <summary>
        /// Retrieves the set of OleDbParameters appropriate for the stored procedure
        /// </summary>
        /// <remarks>
        /// This method will query the database for this information, and then store it in a cache for future requests.
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a OleDbConnection</param>
        /// <param name="spName">the name of the stored prcedure</param>
        /// <returns>an array of OleDbParameters</returns>
        public static OleDbParameter[] GetSpParameterSet(string connectionString, string spName)
        {
            return GetSpParameterSet(connectionString, spName, true);
        }

        /// <summary>
        /// Retrieves the set of OleDbParameters appropriate for the stored procedure
        /// </summary>
        /// <remarks>
        /// This method will query the database for this information, and then store it in a cache for future requests.
        /// </remarks>
        /// <param name="connectionString">a valid connection string for an OleDbConnection</param>
        /// <param name="spName">the name of the stored procedure</param>
        /// <param name="includeReturnValueParameter">a bool value indicating weather the return value parameter should be included in the results</param>
        /// <returns>an array of OleDbParameters</returns>
        public static OleDbParameter[] GetSpParameterSet(string connectionString, string spName, bool includeReturnValueParameter)
        {
            string hashKey = connectionString + ":" + spName + (includeReturnValueParameter ? ":include ReturnValue Parameter" : "");

            OleDbParameter[] cachedParameters;

            cachedParameters = (OleDbParameter[])paramCache[hashKey];

            if (cachedParameters == null)
            {
                cachedParameters = (OleDbParameter[])(paramCache[hashKey] = DiscoverSpParameterSet(connectionString, spName, includeReturnValueParameter));
            }

            return CloneParameters(cachedParameters);
        }

        #endregion Parameter Discovery Functions 
    }
}
