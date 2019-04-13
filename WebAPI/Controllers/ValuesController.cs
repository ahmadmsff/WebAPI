using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using APIDb;

namespace WebAPI.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<barang> Get()
        {
            using(apiEntities apiEntities = new apiEntities())
            {   
                return apiEntities.barangs.ToList();
            }
        }

        public class Barang
        {
            public String ListBarang { get; set; }
        }

        // GET api/values/5
        public HttpResponseMessage Get(int id)
        {
            using (apiEntities apiEntities = new apiEntities())
            {
                var entity = apiEntities.barangs.FirstOrDefault(e => e.id_barang == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                } else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Barang dengan id = " + id.ToString() + " not found");

                }
            }
        }

        // POST api/values
        public HttpResponseMessage Post([FromBody] barang barang)
        {
            try
            {
                using (apiEntities apiEntities = new apiEntities())
                {
                    apiEntities.barangs.Add(barang);
                    apiEntities.SaveChanges();
                    var javaScriptSerializer = new JavaScriptSerializer();
                    Helper.Status status = new Helper.Status() { status = "Success" };
                    var result = javaScriptSerializer.Serialize(status);
                    return new HttpResponseMessage()
                    {
                        Content = new StringContent(result, System.Text.Encoding.UTF8, "application/json")
                    };
                }
            } catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (apiEntities apiEntities = new apiEntities())
                {
                    var data = apiEntities.barangs.FirstOrDefault(e => e.id_barang == id);
                    if (data == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Not Found");
                    }
                    else
                    {
                        apiEntities.barangs.Remove(data);
                        apiEntities.SaveChanges();
                        var javaScriptSerializer = new JavaScriptSerializer();
                        Helper.Status status = new Helper.Status() { status = "Success" };
                        var result = javaScriptSerializer.Serialize(status);
                        return new HttpResponseMessage() {
                            Content = new StringContent(result, System.Text.Encoding.UTF8, "application/json")
                        };
                    }
                }
            } catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e);
            }
        }
    }
}
