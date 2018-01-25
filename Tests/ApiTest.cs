using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MessagesAPI.Controllers;
using MessagesAPI.Models;
using System.Net.Http;
using System.Web.Http.Routing;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class ApiTest
    {
        [TestMethod]
        public void PostReturnsCreatedStatus()
        {
            Message message = new Message() { Text = "Firsttime" };
            MessagesController messagesController = new MessagesController();
            IHttpActionResult actionResult = messagesController.Post(message);
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<Message>;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("DefaultApi", createdResult.RouteName);
            Assert.AreEqual(message.Text, createdResult.Content.Text);

        }



        [TestMethod]
        public void GetReturnsOk()
        {
            Message message = new Message() { Text = "Firsttime" };
            MessagesController messagesController = new MessagesController();
            IHttpActionResult postActionResult = messagesController.Post(message);
            var postCreatedResult = postActionResult as CreatedAtRouteNegotiatedContentResult<Message>;
            IHttpActionResult getActionResult = messagesController.Get(postCreatedResult.Content.Id);
            var getCreatedResult = getActionResult as OkNegotiatedContentResult<Message>;
            Assert.IsNotNull(getCreatedResult);
            Assert.AreEqual(message.Text, getCreatedResult.Content.Text);

        }


        [TestMethod]
        public void PutReturnsOk()
        {
            Message message = new Message() { Text = "Firsttime" };
            MessagesController messagesController = new MessagesController();
            IHttpActionResult postActionResult = messagesController.Post(message);
            var postCreatedResult = postActionResult as CreatedAtRouteNegotiatedContentResult<Message>;
            Message message2 = new Message() { Text = "Second time" };
            IHttpActionResult putActionResult = messagesController.Put(postCreatedResult.Content.Id, message2);
            var putCreatedResult = putActionResult as OkNegotiatedContentResult<Message>;
            Assert.IsNotNull(putCreatedResult);
            Assert.AreEqual(message2.Text, putCreatedResult.Content.Text);

        }

        [TestMethod]
        public void PutReturnsNotFound()
        {
            Message message = new Message() { Text = "Put Text" };
            MessagesController messagesController = new MessagesController();
            IHttpActionResult putActionResult = messagesController.Put(10,message);
            Assert.IsInstanceOfType(putActionResult, typeof(NotFoundResult));


        }

        [TestMethod]
        public void PutReturnsForbidden()
        {
            Message message = new Message() { Text = "Firsttime" };
            MessagesController messagesController = new MessagesController();
            IHttpActionResult postActionResult = messagesController.Post(message);
            var postCreatedResult = postActionResult as CreatedAtRouteNegotiatedContentResult<Message>;

            //changing the username of the message so no one else is allowed to change/delete it
            MessageCollection messageCollection = MessageCollection.GetInstace();
            messageCollection.Messages[postCreatedResult.Content.Id].Username = "RandomUsername";
            Message message2 = new Message() { Text = "Second time" };
            IHttpActionResult putActionResult = messagesController.Put(postCreatedResult.Content.Id, message2);
            var putCreatedResult = putActionResult as StatusCodeResult;
            Assert.AreEqual(putCreatedResult.StatusCode, HttpStatusCode.Forbidden);

        }


        [TestMethod]
        public void DeleteReturnsOk()
        {
            Message message = new Message() { Text = "Firsttime" };
            MessagesController messagesController = new MessagesController();
            IHttpActionResult postActionResult = messagesController.Post(message);
            var postCreatedResult = postActionResult as CreatedAtRouteNegotiatedContentResult<Message>;
            IHttpActionResult deleteActionResult = messagesController.Delete(postCreatedResult.Content.Id);
            Assert.IsInstanceOfType(deleteActionResult, typeof(OkResult));
        }

        [TestMethod]
        public void DeleteReturnsNotFound()
        {
            
            MessagesController messagesController = new MessagesController();
            IHttpActionResult deleteActionResult = messagesController.Delete(100);
            Assert.IsInstanceOfType(deleteActionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DeleteReturnsForbidden()
        {
            Message message = new Message() { Text = "Firsttime" };
            MessagesController messagesController = new MessagesController();
            IHttpActionResult postActionResult = messagesController.Post(message);
            var postCreatedResult = postActionResult as CreatedAtRouteNegotiatedContentResult<Message>;

            //changing the username of the message so no one else is allowed to change/delete it
            MessageCollection messageCollection = MessageCollection.GetInstace();
            messageCollection.Messages[postCreatedResult.Content.Id].Username = "RandomUsername";
           
            IHttpActionResult deleteActionResult = messagesController.Delete(postCreatedResult.Content.Id);
            var deleteCreatedResult = deleteActionResult as StatusCodeResult;
            Assert.AreEqual(deleteCreatedResult.StatusCode, HttpStatusCode.Forbidden);

        }

        [TestMethod]
        public void GetAllNotNull()
        {

            MessagesController messagesController = new MessagesController();
            Message message = new Message() { Text = "Firsttime" };
            for (int i = 0; i < 100; i++)
            {
                messagesController.Post(message);
            }
            IEnumerable<Message> actionResult = messagesController.Get();
            Assert.IsNotNull(actionResult);

        }



      



    }
}
