pagina list {
  "_id": "5b3386872034b06899839a7c",
  "name": "FilmMaker",
  "url": "/filmmakers",
  "_db": "5b3386872034b06899839a65",
  "_entity": {
    "_id": "5b3386872034b06899839a6c",
    "name": "FilmMaker",
    "_db": "5b3386872034b06899839a65",
    "__v": 0,
    "_attrs": [
      {
        "_id": "5b3386872034b06899839a6e",
        "name": "name",
        "required": true,
        "type": "String",
        "_entity": "5b3386872034b06899839a6c",
        "__v": 0
      },
      {
        "_id": "5b3386872034b06899839a6f",
        "name": "surname",
        "type": "String",
        "_entity": "5b3386872034b06899839a6c",
        "__v": 0
      }
    ],
    "_relations": [
      {
        "_id": "5b3386872034b06899839a8f",
        "name": "filmMaker",
        "type": "1:m",
        "required": true,
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
          "_id": "5b3386872034b06899839a6c",
          "name": "FilmMaker",
          "_db": "5b3386872034b06899839a65",
          "__v": 0,
          "_resource": {
            "_id": "5b3386872034b06899839a7c",
            "name": "FilmMaker",
            "url": "/filmmakers",
            "_db": "5b3386872034b06899839a65",
            "_entity": "5b3386872034b06899839a6c",
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
  "_services": [
    {
      "_id": "5b3386872034b06899839a83",
      "name": "create",
      "crudAction": "create",
      "description": "CRUD ACTION create",
      "method": "POST",
      "returnType": "",
      "url": "/",
      "_resource": "5b3386872034b06899839a7c",
      "_roles": [],
      "__v": 0,
      "_params": []
    },
    {
      "_id": "5b3386872034b06899839a84",
      "name": "delete",
      "crudAction": "delete",
      "description": "CRUD ACTION delete",
      "method": "DELETE",
      "returnType": "",
      "url": "/{id}",
      "_resource": "5b3386872034b06899839a7c",
      "_roles": [],
      "__v": 0,
      "_params": []
    },
    {
      "_id": "5b3386872034b06899839a85",
      "name": "get",
      "crudAction": "get",
      "description": "CRUD ACTION get",
      "method": "GET",
      "returnType": "",
      "url": "/{id}",
      "_resource": "5b3386872034b06899839a7c",
      "_roles": [],
      "__v": 0,
      "_params": []
    },
    {
      "_id": "5b3386872034b06899839a86",
      "name": "list",
      "crudAction": "list",
      "description": "CRUD ACTION list",
      "method": "GET",
      "returnType": "",
      "url": "/",
      "_resource": "5b3386872034b06899839a7c",
      "_roles": [],
      "__v": 0,
      "_params": []
    },
    {
      "_id": "5b3386872034b06899839a87",
      "name": "update",
      "crudAction": "update",
      "description": "CRUD ACTION update",
      "method": "POST",
      "returnType": "",
      "url": "/{id}",
      "_resource": "5b3386872034b06899839a7c",
      "_roles": [],
      "__v": 0,
      "_params": []
    }
  ],
  "_relations": [
    {
      "_id": "5b3386872034b06899839a8f",
      "name": "filmMaker",
      "type": "1:m",
      "required": true,
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
        "_id": "5b3386872034b06899839a6c",
        "name": "FilmMaker",
        "_db": "5b3386872034b06899839a65",
        "__v": 0,
        "_resource": {
          "_id": "5b3386872034b06899839a7c",
          "name": "FilmMaker",
          "url": "/filmmakers",
          "_db": "5b3386872034b06899839a65",
          "_entity": "5b3386872034b06899839a6c",
          "_project": "5b30dbb7c3dbdb5d5470bc14",
          "__v": 0,
          "_roles": []
        }
      },
      "__v": 0
    }
  ]
}