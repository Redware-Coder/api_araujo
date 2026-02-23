using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;


namespace SqlAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SqlApp : ControllerBase
    {
        private IConfiguration _configuration;

        public SqlApp(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public class ComunicacaoDto
        {
            public int Comportamento { get; set; }
            public string Loja { get; set; }
            public string Periodo { get; set; }
            public string Mes { get; set; }
            public string Ano { get; set; }
            public string Referencia { get; set; }
            public string DataIni { get; set; }
            public string DataFin { get; set; }
            public string Medida { get; set; }
        }

        //ATUALIZAR
        [HttpPost]
        [Route("UpComunicacao")]
        public IActionResult Comunicacao([FromBody] ComunicacaoDto dto)
        {
            string query = "UPDATE comunicacao SET " +
                                "comportamento = @Comportamento, " +
                                "loja = @loja, " +
                                "periodo = @periodo, " +
                                "mes = @mes, " +
                                "ano = @ano, " +
                                "dataIni = @dataIni, " +
                                "datafin = @dataFin, " +
                                "medida = @medida"
                                ;

            string sqlDatasource = _configuration.GetConnectionString("Conectar");

            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Comportamento", dto.Comportamento);
                    myCommand.Parameters.AddWithValue("@loja", dto.Loja);
                    myCommand.Parameters.AddWithValue("@periodo", dto.Periodo);
                    myCommand.Parameters.AddWithValue("@mes", dto.Mes);
                    myCommand.Parameters.AddWithValue("@ano", dto.Ano);
                    myCommand.Parameters.AddWithValue("@dataIni", dto.DataIni);
                    myCommand.Parameters.AddWithValue("@dataFin", dto.DataFin);
                    myCommand.Parameters.AddWithValue("@medida", dto.Medida);
                    myCommand.ExecuteNonQuery();
                }
            }

            return Ok("Atualizado com sucesso!");
        }

        //PNEUS
        [HttpGet]
        [Route("Pneus")]
        public JsonResult Pneus(int loja, string referencia)
        {
            string query = @"
                            SELECT *
                            FROM produtosMobile
                            WHERE loja = @loja
                              AND saldo <> 0
                              AND (@referencia IS NULL OR @referencia = '' OR referencia = @referencia)";


            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("Conectar");

            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@loja", loja);
                    myCommand.Parameters.AddWithValue("@referencia", referencia ?? "");

                        using (SqlDataReader myReader = myCommand.ExecuteReader())
                        {
                            table.Load(myReader);
                        }
                }
            }

            return new JsonResult(table);
        }

        //PNEUS
        [HttpGet]
        [Route("Pneus2")]
        public JsonResult Pneus2(int loja)
        {
            string query = @"
                            SELECT *
                            FROM produtosMobile
                            WHERE loja = @loja
                              AND saldo <> 0";


            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("Conectar");

            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@loja", loja);

                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        table.Load(myReader);
                    }
                }
            }

            return new JsonResult(table);
        }


        //LOJAS
        [HttpGet]
        [Route("Medidas")]
        public JsonResult Medidas()
        {
            string query = "select * from Medidas";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("Conectar");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        //ESTOQUE LOJAS
        [HttpGet]
        [Route("EstoqueLojas")]
        public JsonResult EstoqueLojas()
        {
            string query = "select * from EstoqueLojas where saldo > 0";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("Conectar");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        //BOLETOS VENCIDOS
        [HttpGet]
        [Route("BoletosVencidos")]
        public JsonResult BoletosVencidos()
        {
            string query = "select * from BoletosVencidos";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("Conectar");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        //GIRO
        [HttpGet]
        [Route("Giro")]
        public JsonResult Giro()
        {
            string query = "select * from Giro where giro <> 0 order by giro desc";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("Conectar");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        //Contas
        [HttpGet]
        [Route("Contas")]
        public JsonResult Contas()
        {
            string query = "select * from financeirolojas where saldo <> 0 and loja not in(1, 3, 4) order by loja";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("Conectar");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        //LOJAS
        [HttpGet]
        [Route("Lojas")]
        public JsonResult Lojas()
        {
            string query = "select * from mobileLojas where total <> '0' order by total desc";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("Conectar");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        //LOJASxLojas
        [HttpGet]
        [Route("LojasxLojas")]
        public JsonResult LojasxLojas()
        {
            string query = "select ml.nome as loja, ml.total as MesAtual, mlp.total as MesPassado from MobileLojas as ml inner join MobileLojaPassada as mlp on mlp.loja = ml.loja where ml.total <> 0";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("Conectar");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        //BUSCAR
        [HttpGet]
        [Route("Dados")]
        public JsonResult Dados()
        {
            string query = "select * from dadosMobileIOS";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("Conectar");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        //BUSCAR
        [HttpGet]
        [Route("Comunicacao")]
        public JsonResult Comunicacao()
        {
            string query = "select * from Comunicacao";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("Conectar");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        //INSERIR
        [HttpPost]
        [Route("Insert")]
        public JsonResult Insert([FromForm] string name)
        {
            string query = "insert into TESTE values((SELECT ISNULL(MAX(id), 0) + 1 FROM TESTE), @name)";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("Conectar");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@name", name);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Adicionado com Sucesso!");
        }

        //ATUALIZAR
        [HttpPost]
        [Route("Update")]
        public JsonResult Update(int id, string nome)
        {
            string query = "update teste set nome=@nome where id=@id";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("Conectar");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", id);
                    myCommand.Parameters.AddWithValue("@nome", nome);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Atualizado com Sucesso!");
        }

        //DELETAR
        [HttpDelete]
        [Route("Delete")]
        public JsonResult Delete(int id)
        {
            string query = "delete from TESTE where id=@id";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("Conectar");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deletado com Sucesso!");
        }
    }
}
