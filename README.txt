install elasticsearch

create new index and mapping
run:
curl -H'Content-Type: application/json' -u elastic:password https://56641ecc747adc2063fe8577b8a09d3b.us-east-1.aws.found.io:9243/recipes/recipe/_bulk?pretty -XPUT -d '{
   "mappings": {
       "recipe": {
           "_source": {
               "enabled": true
           },
           "properties": {
               "title": {
                   "type": "text",
                   "store": true
               },
               "ing": {
                   "type": "text",
                   "store": true
               },
	       "ing_num": {
		   "type": "integer",
		   "store": true
	       }

           }
       }
}
}


now upload json to elasticsearch
run:
curl -H'Content-Type: application/json' -u elastic:password https://56641ecc747adc2063fe8577b8a09d3b.us-east-1.aws.found.io:9243/recipes/recipe/_bulk?pretty -XPUT --data-binary "@recnum_scrap.json"


Now you have a database of recipes and ing
To run query I created a python program that uses elasticsearch package available from pip
