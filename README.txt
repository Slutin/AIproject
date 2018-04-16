install elasticsearch

create new index and mapping
run:
curl -H'Content-Type: application/json' -XPUT 'localhost:9200/recipes' -d'

{
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
               }
  
           }
       }
}
}'

now upload json to elasticsearch
run:
curl -H'Content-Type: application/json' -XPUT 'localhost:9200/recipes/recipe/_bulk?pretty' --data-binary "@rec_scrap.json"

Now you have a database of recipes and ing
To run query I created a python program that uses elasticsearch package available from pip

 
