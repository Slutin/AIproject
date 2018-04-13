import csv
import json

csvfile = open('rec_scrap.csv', 'r')
jsonfile = open('rec_scrap.json', 'w')

id = 1

fieldnames = ("title","ing")
reader = csv.DictReader( csvfile, fieldnames)
for row in reader:
    jsonfile.write('{"index":{"_index":"recipes", "_type":"recipe", "_id": "%s"}}' % id)
    jsonfile.write('\n')
    
    json.dump(row, jsonfile)
    jsonfile.write('\n')
    id += 1
