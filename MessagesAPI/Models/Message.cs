using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace MessagesAPI.Models
{
    [DataContract(Name = "Message")]
    public class Message
    {

        public Message() { }

        public Message(string text)
        {
            this.text = text;
        }

        public Message(int id, string text, string username, DateTime dateTime)
        {
            this.id = id;
            this.text = text;
            this.dateTime = dateTime;
            this.username = username;
        }
        //id of the message
        private int id;
        //the text of the message
        private string text;
        //when the message was generated
        private DateTime dateTime;
        //who wrote the message
        private string username;

        [DataMember]
        public int Id { get => id; set => id = value; }
        [DataMember]
        public string Text { get => text; set => text = value; }
        [DataMember]
        public DateTime DateTime { get => dateTime; set => dateTime = value; }
        [DataMember]
        public string Username { get => username; set => username = value; }
    }
}