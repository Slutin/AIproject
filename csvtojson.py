import csv
import json

csvfile = open('recnum_scrap.csv', 'r')
jsonfile = open('recnum_scrap.json', 'w')

id = 1

fieldnames = ['title', 'ing', 'ing_num']

reader = csv.DictReader(csvfile, fieldnames = fieldnames)
#header = reader.next()

#recIndex = header.index("title")
#ingIndex = header.index("in")
#ing_numIndex = header.index("ing_num")

for row in reader:
    jsonfile.write('{"index":{"_index":"recipes", "_type":"recipe", "_id": "%s"}}' % id)
    jsonfile.write('\n')

    json.dump(row, jsonfile)
    jsonfile.write('\n')
    id += 1
