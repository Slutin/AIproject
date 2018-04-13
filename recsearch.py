from elasticsearch import Elasticsearch

es = Elasticsearch()

res = es.search(index="recipes", body={"query": {
        "constant_score" : {
            "filter" : {
                "terms" : {
                    "ing" : ["rice", "beans", "tomato", "onion", "beef"]
                }
            }
        }
    }})

print("Got %d Hits:" % res['hits']['total'])
for doc in res['hits']['hits']:
    print ("title: %s\Ing: %s" % (doc['_source']['title'], doc['_source']['ing']))
