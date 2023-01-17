using Microsoft.AspNetCore.Mvc;
using PelindoAPI.DBRepo;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PelindoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext dbContext;

        public UserController(UserContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<TblUser> Get()
        {
            return dbContext.TblUsers.ToList();
        }

        // GET: api/user/getDataUser/{param}
        [HttpGet]
        [Route("getDataUser/{param}")]
        public IEnumerable<TblUser> getDataUser(string param)
        {
            if(param == "all")
            {
                return dbContext.TblUsers.ToList();
            }
            return dbContext.TblUsers.Where(x => x.Userid == Int32.Parse(param)).ToList();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/user/setDataUser
        [HttpPost]
        [Route("setDataUser")]
        public HttpResponseMessage Post([FromBody] TblUser formInput)
        {
            try
            {
                if(formInput.Userid == 0)
                {
                    dbContext.TblUsers.Add(formInput);

                    dbContext.SaveChanges();
                } else
                {
                    var dataUser = dbContext.TblUsers.Where(x => x.Userid == formInput.Userid).First();

                    dataUser.Namalengkap = formInput.Namalengkap;
                    dataUser.Password = formInput.Password;
                    dataUser.Username = formInput.Username;
                    dataUser.Status = formInput.Status;

                    dbContext.SaveChanges();
                }
                return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("oke") };
            }
            catch (Exception e)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent(e.Message) };
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/user/delDataUser
        [HttpDelete]
        [Route("delDataUser/{userId}")]
        public HttpResponseMessage Delete(int userId)
        {
            try
            {
                dbContext.Remove(dbContext.TblUsers.Single(a => a.Userid == userId));
                dbContext.SaveChanges();

                return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("oke") };

            }
            catch(Exception e)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent(e.Message) };
            }
        }
    }
}
