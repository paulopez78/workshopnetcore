# mongodb database commands
use votingapp
db.votingapp.insert({"options":"c#"})
db.votingapp.find()

# mongodb replicaset commands
rs.slaveOk()
rs.config()

# mongodb replicaset manual configuration
config = {
      "_id" : "rs0",
      "members" : [
          {
              "_id" : 0,
              "host" : "mongodb-0:27017"
          },
          {
              "_id" : 1,
              "host" : "mongodb-1:27017"
          },
          {
              "_id" : 2,
              "host" : "mongodb-2:27017"
          }
      ]
  }

rs.initiate(config)