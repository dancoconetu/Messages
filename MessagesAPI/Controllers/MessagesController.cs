using MessagesAPI.Models;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Results;
using MessagesAPI.Misc;

namespace MessagesAPI.Controllers
{   
    public class MessagesController : ApiController
    {

        MessageCollection messageCollection = MessageCollection.GetInstace();
        /// <summary> 
        /// Getting all the messages sent, either sorted by id, date or username
        /// </summary>
        /// <param name="sort"> Choose either date for sorting by date or username for sorting by username or none for default </param> 
        /// <returns></returns>
        // GET: api/Message
        [SwaggerResponse(HttpStatusCode.Created, Description = "The messages were retrieved succesfully")]
        public IEnumerable<Message> Get(string sort = "none")
        {
            switch (sort)
            {
                case "date":
                    return messageCollection.GetAllMessagesSortedByDate();
                case "username":
                    return messageCollection.GetAllMessagesSortedByUsername();
                default:
                    return messageCollection.GetAllMessages();
            }

        }

        /// <summary> 
        /// Get a single message by id
        /// </summary>
        /// <param name="id"> The id of the message to be returned </param> 
        /// <returns></returns>
        // GET: api/Message/5
        [SwaggerResponse(HttpStatusCode.OK, Description = "The message was retrieved succesfully")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "The message could not be found")]
        public IHttpActionResult Get(int id)
        {
            Message message = messageCollection.GetMessage(id);
            if (message != null)
                return Ok(message);
            else
                return NotFound();
        }

        /// <summary> 
        /// Add a new message
        /// </summary>
        /// <param name="receivedMessage"> Message object containing the text </param> 
        /// <returns></returns>
        // POST: api/Message
        [SwaggerResponse(HttpStatusCode.Created, Description = "The message was added to the collection")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Description = "The message could not be added to the collection")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Description = "The user is not authorized")]
        [BasicAuthentication]
        public IHttpActionResult Post([FromBody]Message receivedMessage)
        {
            string username = Thread.CurrentPrincipal.Identity.Name;
            try
            {
                receivedMessage.Username = username;
                Message message = messageCollection.AddMessage(receivedMessage);
                //var responseMessage = Request.CreateResponse(HttpStatusCode.Created, message);
                //responseMessage.Headers.Location = new Uri(Request.RequestUri + "/" + (message.Id).ToString());
                return CreatedAtRoute("DefaultApi", new { id = message.Id }, message);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return StatusCode(HttpStatusCode.InternalServerError);
            }

        }

        /// <summary> 
        /// Update a message by ID
        /// </summary>
        /// <param name="id"> Id of the message </param> 
        /// <param name="message"> Message object containing the updated text </param> 
        /// <returns></returns>
        // PUT: api/Message/5
        [SwaggerResponse(HttpStatusCode.OK, Description = "The update was succesfull")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "The message was not found")]
        [SwaggerResponse(HttpStatusCode.Forbidden, Description = "The user has no rights to update the message")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Description = "The user is not authorized")]
        [BasicAuthentication]
        public IHttpActionResult Put(int id, [FromBody]Message message)
        {
            string username = Thread.CurrentPrincipal.Identity.Name;
            if (messageCollection.GetMessage(id) != null && messageCollection.GetMessage(id).Username == username)
            {
                messageCollection.EditMessage(id, message);
                return Ok(messageCollection.GetMessage(id));
            }
            else if (messageCollection.GetMessage(id) == null)
            {
                return NotFound();
            }
            else
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }
        }

        /// <summary> 
        /// Delete a message by ID
        /// </summary>
        /// <param name="id"> Id of the message </param> 
        /// <returns></returns>
        // DELETE: api/Message/5
        [SwaggerResponse(HttpStatusCode.OK, Description = "Delete was succesfull")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "The message was not found")]
        [SwaggerResponse(HttpStatusCode.Forbidden, Description = "The user has no rights to delete the message")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Description = "The user is not authorized")]
        [BasicAuthentication]
        public IHttpActionResult Delete(int id)
        {
            string username = Thread.CurrentPrincipal.Identity.Name;
            if (messageCollection.GetMessage(id) != null && messageCollection.GetMessage(id).Username == username)
            {
                messageCollection.DeleteMessage(id);
                return Ok();
            }
            else if (messageCollection.GetMessage(id) == null)
            {
                return NotFound();
            }
            else
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }



        }

    }
}
