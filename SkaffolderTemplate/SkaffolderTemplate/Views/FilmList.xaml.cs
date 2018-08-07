pagina list {
  "_id": "5b3386872034b06899839a98",
  "name": "Film",
  "url": "/films",
  "_db": "5b3386872034b06899839a65",
  "_entity": {
    "_id": "5b3386872034b06899839a6b",
    "name": "Film",
    "_db": "5b3386872034b06899839a65",
    "__v": 0,
    "_attrs": [
      {
        "_id": "5b3386872034b06899839a70",
        "name": "genre",
        "type": "String",
        "_entity": "5b3386872034b06899839a6b",
        "__v": 0,
        "_enum": [
          {
            "_id": "5b3386872034b06899839a7d",
            "name": "Action",
            "_attr": "5b3386872034b06899839a70",
            "__v": 0
          },
          {
            "_id": "5b3386872034b06899839a7e",
            "name": "Crime",
            "_attr": "5b3386872034b06899839a70",
            "__v": 0
          },
          {
            "_id": "5b3386872034b06899839a7f",
            "name": "Fantasy",
            "_attr": "5b3386872034b06899839a70",
            "__v": 0
          },
          {
            "_id": "5b3386872034b06899839a80",
            "name": "Horror",
            "_attr": "5b3386872034b06899839a70",
            "__v": 0
          }
        ]
      },
      {
        "_id": "5b3386872034b06899839a71",
        "name": "title",
        "required": true,
        "type": "String",
        "_entity": "5b3386872034b06899839a6b",
        "__v": 0
      },
      {
        "_id": "5b3386872034b06899839a72",
        "name": "year",
        "type": "Number",
        "_entity": "5b3386872034b06899839a6b",
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
      },
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
      "_id": "5b3386872034b06899839a9c",
      "name": "create",
      "crudAction": "create",
      "description": "CRUD ACTION create",
      "method": "POST",
      "returnType": "",
      "url": "/",
      "_resource": "5b3386872034b06899839a98",
      "_roles": [],
      "__v": 0,
      "_params": []
    },
    {
      "_id": "5b3386872034b06899839a9d",
      "name": "delete",
      "crudAction": "delete",
      "description": "CRUD ACTION delete",
      "method": "DELETE",
      "returnType": "",
      "url": "/{id}",
      "_resource": "5b3386872034b06899839a98",
      "_roles": [],
      "__v": 0,
      "_params": []
    },
    {
      "_id": "5b3386872034b06899839a9e",
      "name": "findBycast",
      "crudAction": "findBycast",
      "description": "CRUD ACTION findBycast",
      "method": "GET",
      "returnType": "",
      "url": "/findBycast/{key}",
      "_resource": "5b3386872034b06899839a98",
      "_roles": [],
      "__v": 0,
      "_params": []
    },
    {
      "_id": "5b3386872034b06899839a9f",
      "name": "findByfilmMaker",
      "crudAction": "findByfilmMaker",
      "description": "CRUD ACTION findByfilmMaker",
      "method": "GET",
      "returnType": "",
      "url": "/findByfilmMaker/{key}",
      "_resource": "5b3386872034b06899839a98",
      "_roles": [],
      "__v": 0,
      "_params": []
    },
    {
      "_id": "5b3386872034b06899839aa0",
      "name": "get",
      "crudAction": "get",
      "description": "CRUD ACTION get",
      "method": "GET",
      "returnType": "",
      "url": "/{id}",
      "_resource": "5b3386872034b06899839a98",
      "_roles": [],
      "__v": 0,
      "_params": []
    },
    {
      "_id": "5b3386872034b06899839aa1",
      "name": "list",
      "crudAction": "list",
      "description": "CRUD ACTION list",
      "method": "GET",
      "returnType": "",
      "url": "/",
      "_resource": "5b3386872034b06899839a98",
      "_roles": [],
      "__v": 0,
      "_params": []
    },
    {
      "_id": "5b3386872034b06899839aa2",
      "name": "update",
      "crudAction": "update",
      "description": "CRUD ACTION update",
      "method": "POST",
      "returnType": "",
      "url": "/{id}",
      "_resource": "5b3386872034b06899839a98",
      "_roles": [],
      "__v": 0,
      "_params": []
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
    },
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