using invoicesManagement.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlClient;

namespace invoicesManagement.Data
{
    public class InvoiceData
    {
        //FUNCION PARA LISTAR LAS FACTURAS
        public List<InvoiceModel> List(int? id = null)
        {
            var invoicesList = new List<InvoiceModel>();

            var conn = new Connection();

            using (var connection = new SqlConnection(conn.getSqlString()))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("listInvoices", connection);
                if (id.HasValue)
                {
                    cmd.Parameters.AddWithValue("id", id);
                }
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        invoicesList.Add(new InvoiceModel()
                        {
                            id = Convert.ToInt32(dr["id"]),
                            invoiceName = dr["invoiceName"].ToString(),
                            invoiceNumber = dr["invoiceNumber"].ToString(),
                            invoiceExpiration = Convert.ToDateTime(dr["invoiceExpiration"]),
                            invoiceTotal = dr["invoiceTotal"].ToString(),
                            invoiceDescription = dr["invoiceDescription"].ToString(),
                            createdAt = Convert.ToDateTime(dr["createdAt"]),
                            updatedAt = Convert.ToDateTime(dr["updatedAt"]),
                        });
                    }
                }
            }

            return invoicesList;
        }

        //LISTA DE ESTADOS PARA FUNCION QUE VERIFICA NUMEROS DE FACTURAS YA REGISTRADAS
        public enum StatusCheckNumberInvoice
        {
            Found,
            NotFound,
            Error
        }

        //FUNCION PARA DETERMINAR SI EL NUMERO DE FACTURA SE ENCUENTRA REGISTRADO
        public StatusCheckNumberInvoice checkNumberInvoice(string invoiceNumber, int? id = null)
        {
            try
            {
                var conn = new Connection();

                using (var connection = new SqlConnection(conn.getSqlString()))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("checkInvoiceNumber", connection);
                    cmd.Parameters.AddWithValue("invoiceNumber", invoiceNumber);
                    if (id.HasValue)
                    {
                        cmd.Parameters.AddWithValue("id", id);
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    int count = (int)cmd.ExecuteScalar();

                    if (count > 0)
                    {
                        return StatusCheckNumberInvoice.Found;
                    }
                    else
                    {
                        return StatusCheckNumberInvoice.NotFound;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCheckNumberInvoice.Error;
            }
        }

        //STATUSES TRANSACTIONS, INSERT, EDIT, DELETE
        public enum statusTransaction
        {
            Success,
            Duplicated,
            Error
        }

        //FUNCION PARA REGISTRAR FACTURAS
        public statusTransaction insertInvoice(InvoiceModel invoice)
        {
            //VERIFICAR SI EL NUMERO DE FACTURA YA ESTA REGISTRADO
            if (checkNumberInvoice(invoice.invoiceNumber) == StatusCheckNumberInvoice.Found)
            {
                return statusTransaction.Duplicated;
            }

            if (checkNumberInvoice(invoice.invoiceNumber) == StatusCheckNumberInvoice.Error)
            {
                return statusTransaction.Error;
            }

            try
            {
                var conn = new Connection();

                using (var connection = new SqlConnection(conn.getSqlString()))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("insertInvoices", connection);
                    cmd.Parameters.AddWithValue("invoiceName", invoice.invoiceName);
                    cmd.Parameters.AddWithValue("invoiceNumber", invoice.invoiceNumber);
                    cmd.Parameters.AddWithValue("invoiceExpiration", invoice.invoiceExpiration);
                    cmd.Parameters.AddWithValue("invoiceTotal", invoice.invoiceTotal);
                    cmd.Parameters.AddWithValue("invoiceDescription", invoice.invoiceDescription);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                }

                return statusTransaction.Success;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return statusTransaction.Error;
            }
        }

        //FUNCION PARA EDITAR FACTURAS
        public statusTransaction editInvoice(InvoiceModel invoice)
        {
            //VERIFICAR SI EL NUMERO DE FACTURA ESTA REGISTRADO CON OTRO ID
            if (checkNumberInvoice(invoice.invoiceNumber, invoice.id) == StatusCheckNumberInvoice.Found)
            {
                return statusTransaction.Duplicated;
            }

            if (checkNumberInvoice(invoice.invoiceNumber, invoice.id) == StatusCheckNumberInvoice.Error)
            {
                return statusTransaction.Error;
            }

            try
            {
                var conn = new Connection();

                using (var connection = new SqlConnection(conn.getSqlString()))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("editInvoices", connection);
                    cmd.Parameters.AddWithValue("id", invoice.id);
                    cmd.Parameters.AddWithValue("invoiceName", invoice.invoiceName);
                    cmd.Parameters.AddWithValue("invoiceNumber", invoice.invoiceNumber);
                    cmd.Parameters.AddWithValue("invoiceExpiration", invoice.invoiceExpiration);
                    cmd.Parameters.AddWithValue("invoiceTotal", invoice.invoiceTotal);
                    cmd.Parameters.AddWithValue("invoiceDescription", invoice.invoiceDescription);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                }
                return statusTransaction.Success;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return statusTransaction.Error;
            }
        }

        //FUNCION PARA MARCAR ESTADO ELIMINADO (invoiceStatus = 0) EN LAS FACTURAS
        public bool updateDeletedInvoices(int id)
        {
            bool result;

            try
            {
                var conn = new Connection();

                using (var connection = new SqlConnection(conn.getSqlString()))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("updateDeletedInvoices", connection);
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                }

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                result = false;
            }

            return result;
        }
    }
}
