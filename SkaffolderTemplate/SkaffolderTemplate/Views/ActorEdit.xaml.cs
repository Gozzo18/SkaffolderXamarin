pagina edit {
  "_id": "5b3386872034b06899839a82",
  "name": "Actor",
  "url": "/actors",
  "_db": "5b3386872034b06899839a65",
  "_entity": {
    "_id": "5b3386872034b06899839a6a",
    "name": "Actor",
    "_db": "5b3386872034b06899839a65",
    "__v": 0,
    "_attrs": [
      {
        "_id": "5b3386872034b06899839a79",
        "name": "birthDate",
        "type": "Date",
        "_entity": "5b3386872034b06899839a6a",
        "__v": 0
      },
      {
        "_id": "5b3386872034b06899839a7a",
        "name": "name",
        "required": true,
        "type": "String",
        "_entity": "5b3386872034b06899839a6a",
        "__v": 0
      },
      {
        "_id": "5b3386872034b06899839a7b",
        "name": "surname",
        "type": "String",
        "_entity": "5b3386872034b06899839a6a",
        "__v": 0
      }
    ],
    "_relations": [
      {
        "_id": "5b3386872034b06899839a8e",
        "name": "cast",
        "type": "m:m",
        "_ent1": {
          "_id": "5b3386872034b06899839a6b",
          "name": "Film",
          "_db": "5b3386872034b06899839a65",
          "__v": 0,
          "_resource": {
            "_id": "5b3386872034b06899839a98",
            "name": "Film",
            "url": "/films",
            "_db": "5b3386872034b06899839a65",
            "_entity": "5b3386872034b06899839a6b",
            "_project": "5b30dbb7c3dbdb5d5470bc14",
            "__v": 0,
            "_roles": []
          }
        },
        "_ent2": {
          "_id": "5b3386872034b06899839a6a",
          "name": "Actor",
          "_db": "5b3386872034b06899839a65",
          "__v": 0,
          "_resource": {
            "_id": "5b3386872034b06899839a82",
            "name": "Actor",
            "url": "/actors",
            "_db": "5b3386872034b06899839a65",
            "_entity": "5b3386872034b06899839a6a",
            "_project": "5b30dbb7c3dbdb5d5470bc14",
            "__v": 0,
            "_roles": []
          }
        },
        "__v": 0
      }
    ]
  },
  "_project": "5b30dbb7c3dbdb5d5470bc14",
  "_roles": [],
  "__v": 0,
  "_relations": [
    {
      "_id": "5b3386872034b06899839a8e",
      "name": "cast",
      "type": "m:m",
      "_ent1": {
        "_id": "5b3386872034b06899839a6b",
        "name": "Film",
        "_db": "5b3386872034b06899839a65",
        "__v": 0,
        "_resource": {
          "_id": "5b3386872034b06899839a98",
          "name": "Film",
          "url": "/films",
          "_db": "5b3386872034b06899839a65",
          "_entity": "5b3386872034b06899839a6b",
          "_project": "5b30dbb7c3dbdb5d5470bc14",
          "__v": 0,
          "_roles": []
        }
      },
      "_ent2": {
        "_id": "5b3386872034b06899839a6a",
        "name": "Actor",
        "_db": "5b3386872034b06899839a65",
        "__v": 0,
        "_resource": {
          "_id": "5b3386872034b06899839a82",
          "name": "Actor",
          "url": "/actors",
          "_db": "5b3386872034b06899839a65",
          "_entity": "5b3386872034b06899839a6a",
          "_project": "5b30dbb7c3dbdb5d5470bc14",
          "__v": 0,
          "_roles": []
        }
      },
      "__v": 0
    }
  ],
  "_services": [
    {
      "_id": "5b3386872034b06899839a93",
      "name": "create",
      "crudAction": "create",
      "description": "CRUD ACTION create",
      "method": "POST",
      "returnType": "",
      "url": "/",
      "_resource": "5b3386872034b06899839a82",
      "_roles": [],
      "__v": 0,
      "_params": []
    },
    {
      "_id": "5b3386872034b06899839a94",
      "name": "delete",
      "crudAction": "delete",
      "description": "CRUD ACTION delete",
      "method": "DELETE",
      "returnType": "",
      "url": "/{id}",
      "_resource": "5b3386872034b06899839a82",
      "_roles": [],
      "__v": 0,
      "_params": [
        {
          "_id": "5b3386872034b06899839a9b",
          "name": "id",
          "description": "Id",
          "type": "ObjectId",
          "_service": "5b3386872034b06899839a94",
          "__v": 0
        }
      ]
    },
    {
      "_id": "5b3386872034b06899839a95",
      "name": "get",
      "crudAction": "get",
      "description": "CRUD ACTION get",
      "method": "GET",
      "returnType": "",
      "url": "/{id}",
      "_resource": "5b3386872034b06899839a82",
      "_roles": [],
      "__v": 0,
      "_params": [
        {
          "_id": "5b3386872034b06899839a99",
          "name": "id",
          "description": "Id ",
          "type": "ObjectId",
          "_service": "5b3386872034b06899839a95",
          "__v": 0
        }
      ]
    },
    {
      "_id": "5b3386872034b06899839a96",
      "name": "list",
      "crudAction": "list",
      "description": "CRUD ACTION list",
      "method": "GET",
      "returnType": "",
      "url": "/",
      "_resource": "5b3386872034b06899839a82",
      "_roles": [],
      "__v": 0,
      "_params": []
    },
    {
      "_id": "5b3386872034b06899839a97",
      "name": "update",
      "crudAction": "update",
      "description": "CRUD ACTION update",
      "method": "POST",
      "returnType": "",
      "url": "/{id}",
      "_resource": "5b3386872034b06899839a82",
      "_roles": [],
      "__v": 0,
      "_params": [
        {
          "_id": "5b3386872034b06899839a9a",
          "name": "id",
          "description": "Id",
          "type": "ObjectId",
          "_service": "5b3386872034b06899839a97",
          "__v": 0
        }
      ]
    }
  ]
}