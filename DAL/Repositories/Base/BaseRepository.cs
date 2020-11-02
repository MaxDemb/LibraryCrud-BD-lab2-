using Npgsql;
using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories.Base
{
    public abstract class BaseRepository
    {
        public readonly string sqlUpdate = "Update @table set @field_to_update = @new_value where @field_to_find = @old_value";
        public readonly string sqlRandomString = "chr(trunc(65 + random() * 50)::int) || chr(trunc(65 + random() * 25)::int) || chr(trunc(65 + random() * 25)::int) || chr(trunc(65 + random() * 25)::int)";
        public readonly string sqlRandomInteger = "trunc(random()*1000)::int";
        public readonly string sqlRandomDate = "timestamp '2014-01-10 20:00:00' + random() * (timestamp '2014-01-20 20:00:00' - timestamp '2014-01-10 10:00:00')";
        public readonly string sqlRandomBoolean = "trunc(random()*2)::int::boolean";

        protected NpgsqlConnection sqlConnection;
        protected BaseRepository(NpgsqlConnection sqlConnection)
        {
            this.sqlConnection = sqlConnection;
        }
        public void Update(string table, string field_to_update, string new_value, string field_to_find, string old_value)
        {
            sqlConnection.Open();

            StringBuilder updateString = new StringBuilder("Update", 200);


            int new_int;
            if(!Int32.TryParse(new_value,out new_int))
            {
                new_value = "'" + new_value + "'";
            }
            if (!Int32.TryParse(old_value, out new_int))
            {
                old_value = "'" + old_value + "'";
            }

            updateString.AppendFormat(" {0} set {1} = {2} where {3} = {4}", table, field_to_update, new_value, field_to_find, old_value);

            
            using var cmd = new NpgsqlCommand(updateString.ToString(), sqlConnection);

            


            
            try
            {
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

    }
}
