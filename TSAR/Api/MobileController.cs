    using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using System.Web.Http.OData.Routing;
using TSAR.Models;
using Microsoft.Data.OData;

namespace TSAR.Api
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using TSAR.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Client>("Mobile");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class MobileController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();

        // GET: odata/Mobile
        public async Task<IHttpActionResult> GetMobile(ODataQueryOptions<Client> queryOptions)
        {
            // validate the query.
            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

            // return Ok<IEnumerable<Client>>(clients);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // GET: odata/Mobile(5)
        public async Task<IHttpActionResult> GetClient([FromODataUri] int key, ODataQueryOptions<Client> queryOptions)
        {
            // validate the query.
            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

            // return Ok<Client>(client);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // PUT: odata/Mobile(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Client> delta)
        {
            Validate(delta.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get the entity here.

            // delta.Put(client);

            // TODO: Save the patched entity.

            // return Updated(client);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // POST: odata/Mobile
        public async Task<IHttpActionResult> Post(Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Add create logic here.

            // return Created(client);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // PATCH: odata/Mobile(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Client> delta)
        {
            Validate(delta.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get the entity here.

            // delta.Patch(client);

            // TODO: Save the patched entity.

            // return Updated(client);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // DELETE: odata/Mobile(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            // TODO: Add delete logic here.

            // return StatusCode(HttpStatusCode.NoContent);
            return StatusCode(HttpStatusCode.NotImplemented);
        }
    }
}
