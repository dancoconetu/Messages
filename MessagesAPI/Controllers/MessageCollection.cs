using System;
using System.Collections.Generic;
using System.Linq;
using MessagesAPI.Models;

namespace MessagesAPI.Controllers
{   
    public class MessageCollection
    {
        private List<Message> messages;
        public static MessageCollection instance = null;
        
        private MessageCollection()
        {
            messages = new List<Message>();
        }
        //singleton pattern used for this class
        public static MessageCollection GetInstace()
        {
            if (instance == null)
                instance = new MessageCollection();
            return instance;
        }

        public List<Message> Messages { get => messages; set => messages = value; }

        public Message AddMessage(Message message)
        {
            message.DateTime = DateTime.Now;
            message.Id = Messages.Count;
            Messages.Add(message);
            return message;
        }

        public void DeleteMessage(int id)
        {
            Messages[id] = null;

        }

        public IEnumerable<Message> GetAllMessages()
        {
            return Messages.Where(m => m != null);
        }

        public IEnumerable<Message> GetAllMessagesSortedByDate()
        {
            return Messages.Where(m => m != null).OrderBy(m => m.DateTime);
        }

        public IEnumerable<Message> GetAllMessagesSortedByUsername()
        {
            return Messages.Where(m => m != null).OrderBy(m => m.Username);
        }

        public Message GetMessage(int id)
        {
            try
            {
                return Messages[id];
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool EditMessage(int id, Message message)
        {
            Messages[id].Text = message.Text;
            Messages[id].DateTime = DateTime.Now;

            return true;
        }
    }
}