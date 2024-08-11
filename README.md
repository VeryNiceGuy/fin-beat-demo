# fin-beat-demo
 
Insert some items into database using POST method and the following json:
[{"1": "value1"}, {"2": "value2"}, {"3": "value3"}]

Now get them back with GET method and optional filtration by id, code or value:
[
  {
    "id": 1,
    "code": 1,
    "value": "value1"
  },
  {
    "id": 2,
    "code": 2,
    "value": "value2"
  },
  {
    "id": 3,
    "code": 3,
    "value": "value3"
  }
]

You can find the second and the third assignments in assignment_2.sql and assignment_3.sql respectively.