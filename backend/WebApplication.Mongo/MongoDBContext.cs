using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication.Mongo.Models;

namespace WebApplication.Mongo
{
    public class MongoDBContext
    {
        public IMongoCollection<UserDO> Users { get; private set; }
        public IMongoCollection<GroupDO> Groups { get; private set; }
        public IMongoCollection<MeetingDO> Meetings { get; private set; }
        public IMongoCollection<InvitationDO> Invitations { get; private set; }
        public IMongoCollection<MovieDO> Movies { get; private set; }

        private readonly MongoConfiguration _configuration;

        public MongoDBContext(IOptions<MongoConfiguration> configuration)
        {
            _configuration = configuration.Value;
            var client = new MongoClient(_configuration.ConnectionString);
            var database = client.GetDatabase(_configuration.DatabaseName);

            Users = database.GetCollection<UserDO>(_configuration.UserCollectionName);
            Groups = database.GetCollection<GroupDO>(_configuration.GroupCollectionName);
            Meetings = database.GetCollection<MeetingDO>(_configuration.MeetingCollectionName);
            Invitations = database.GetCollection<InvitationDO>(_configuration.InvitationCollectionName);
            Movies = database.GetCollection<MovieDO>(_configuration.MovieCollectionName);
        }
    }
}
