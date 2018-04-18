from elasticsearch import Elasticsearch

es = Elasticsearch(["https://56641ecc747adc2063fe8577b8a09d3b.us-east-1.aws.found.io"],
    verify_certs=False,
    http_auth = ('elastic', 'password'),
    scheme = "https",
    port = 9243
    )

ingredients = raw_input("Enter ingredients: ")

res = es.search(index="recipes", size = 100, body={"query": {
        "bool" : {
            "must" : {
                "terms" : {
                    "ing" : ["rice", "beans", "tomato", "onion", "beef"]
                },
                "terms" : {
                    "ing_num": [7]
                }
            }
        }
    }})

print("Got %d Hits:" % res['hits']['total'])
for doc in res['hits']['hits']:
    print ("title: %s\nIng: %s\nnum: %s" % (doc['_source']['title'], doc['_source']['ing'], doc['_source']['ing_num']))
