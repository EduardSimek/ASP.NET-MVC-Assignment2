using CRMLeades.Models;
using System.Data;
using System.Data.SqlClient;

namespace CRMLeades.Data
{
    public class LeadRepository
    {
        private SqlConnection _connection;

        public LeadRepository()
        {
            string connStr = "server=DESKTOP-SPKDOT3\\SQLEXPRESS;database=CMRLeads; " +
                "Integrated Security=true;TrustServerCertificate=true;";

            _connection = new SqlConnection(connStr);
        }

        public List<LeadsEntity> GetAllLeads()
        {
            List<LeadsEntity> leadListEntity = new List<LeadsEntity>();

            SqlCommand cmd = new SqlCommand("GetAllLeads", _connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            dataAdapter.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                leadListEntity.Add(
                    new LeadsEntity
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        LeadDate = Convert.ToDateTime(dr["LeadDate"]),
                        Name = dr["Name"].ToString(),
                        EmailAddress = dr["EmailAddress"].ToString(),
                        Mobile = dr["Mobile"].ToString(),
                        LeadSource = dr["LeadSource"].ToString(),
                        LeadStatus = dr["LeadStatus"].ToString(),
                        NextFollowUpDate = Convert.ToDateTime(dr["NextFollowUpDate"]),
                    }
                );
            }
            return leadListEntity;

        }

        public bool AddLead(LeadsEntity lead)
        {
            SqlCommand cmd = new SqlCommand("AddLead", _connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@LeadDate", lead.LeadDate);
            cmd.Parameters.AddWithValue("@Name", lead.Name);
            cmd.Parameters.AddWithValue("@EmailAddress", lead.EmailAddress);
            cmd.Parameters.AddWithValue("@Mobile", lead.Mobile);
            cmd.Parameters.AddWithValue("@LeadSource", lead.LeadSource);
            cmd.Parameters.AddWithValue("@LeadStatus", lead.LeadStatus);
            cmd.Parameters.AddWithValue("@NextFollowUpDate", lead.NextFollowUpDate);

            _connection.Open();
            int i = cmd.ExecuteNonQuery();
            _connection.Close();

            if (i >= 1) return true;
            else return false;
        }

        public LeadsEntity GetLeadById22(int Id)
        {
            LeadsEntity leadListEntity = new LeadsEntity();

            SqlCommand cmd = new SqlCommand("GetLeadDetailsById", _connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter;

            cmd.Parameters.Add(new SqlParameter("@Id", Id));

            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            dataAdapter.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                leadListEntity = new LeadsEntity
                {
                    Id = Convert.ToInt32(dr["id"]),
                    LeadDate = Convert.ToDateTime(dr["LeadDate"]),
                    Name = dr["Name"].ToString(),
                    EmailAddress = dr["EmailAddress"].ToString(),
                    Mobile = dr["Mobile"].ToString(),
                    LeadSource = dr["LeadSource"].ToString(),
                    LeadStatus = dr["LeadStatus"].ToString(),
                    NextFollowUpDate = Convert.ToDateTime(dr["NextFollowUpDate"]),
                };
            }
            return leadListEntity;

        }

        public bool EditLeadDetails(int Id, LeadsEntity lead)
        {
            SqlCommand cmd = new SqlCommand("EditLead", _connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id",Id);

            cmd.Parameters.AddWithValue("@LeadDate", lead.LeadDate);
            cmd.Parameters.AddWithValue("@Name", lead.Name);
            cmd.Parameters.AddWithValue("@EmailAddress", lead.EmailAddress);
            cmd.Parameters.AddWithValue("@Mobile", lead.Mobile);
            cmd.Parameters.AddWithValue("@LeadSource", lead.LeadSource);
            cmd.Parameters.AddWithValue("@LeadStatus", lead.LeadStatus);
            cmd.Parameters.AddWithValue("@NextFollowUpDate", lead.NextFollowUpDate);

            _connection.Open();
            int i = cmd.ExecuteNonQuery();
            _connection.Close();

            if (i >= 1) return true;
            else return false;
        }

        public bool DeleteLeadDetails(int Id, LeadsEntity lead)
        {
            SqlCommand cmd = new SqlCommand("DeleteLead", _connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", Id);

            _connection.Open();
            int i = cmd.ExecuteNonQuery();
            _connection.Close();

            if (i >= 1) return true;
            else return false;
        }





    }
}
