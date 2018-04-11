import csv

with open('users.csv') as csvfile:
    reader = csv.DictReader(csvfile)
    for row in reader:
         print(row['name'], row['age'])