to query run
curl -u elastic:password https://56641ecc747adc2063fe8577b8a09d3b.us-east-1.aws.found.io:9243/recipes/_search?pretty -XGET -b '
{"query": {
        "bool" : {
            "must" : {
                "terms" : {
                    "ing" : "beef"
                },
                "terms" : {
                    "ing_num": [7]
                }
            }
        }
    }}'
