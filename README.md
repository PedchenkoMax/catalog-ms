# 📱 Catalog microservice

## 📌 Table of contents
- [❓ Main responsibilities](#-main-responsibilities)
- [🧱 Technology stack](#-technology-stack)
- [🚀 How to start](#-how-to-start)

- [📡 API documentation](#-api-documentation)
  - [💸 Product](#-product-endpoints)
    - [📁Retrieve products](#-retrieve-products)
    - [📎 Retrieve product by Id](#-retrieve-product-by-id)
    - [Object](#-product-object)
  - [🔖 Category](#-category-endpoints)
    - [📁Retrieve all categories](#-retrieve-all-categories)
    - [📎 Retrieve category by Id](#-retrieve-category-by-id)
    - [Object](#-category-object)
  - [💼 Brand](#-brand-endpoints)
    - [📁Retrieve all brands](#-retrieve-all-brands)
    - [📎 Retrieve brand by Id](#-retrieve-brand-by-id)
    - [Object](#-brand-object)

  
- [📮 Event contracts](#-event-contracts)
  - [💸 Product](#-product)
    - [📃 Created](#-product-created-event)
    - [✏️ Updated](#-product-updated-event)
    - [🚫 Deleted](#-product-deleted-event)
  - [🖼️ Product Image](#-product-image)
    - [📃 Created](#-product-image-created-event)
    - [✏️ Updated](#-product-image-updated-event)
    - [🚫 Deleted](#-product-image-deleted-event)
  - [🔖 Category](#-category)
    - [📃 Created](#-category-created-event)
    - [✏️ Updated](#-category-updated-event)
    - [🚫 Deleted](#-category-deleted-event)
  - [🛍️ Brand](#-brand)
    - [📃 Created](#-brand-created-event)
    - [✏️ Updated](#-brand-updated-event)
    - [🚫 Deleted](#-brand-deleted-event)


## ❓ Main responsibilities

* ### Retrieving and displaying product information and details.
* ### Filtering, ordering and searching for products based on various criteria.

## 🧱 Technology stack

- **SDK:** `.NET 7`
- **Framework:** `ASP .NET`
- **Persistence:**
  - Database: `MS SQL Server`
  - ORM: `Entity Framework Core`
- **Messaging:**
  - Service Bus: `MassTransit`
  - Message Broker: `RabbitMQ`
- **Testing:**
  - Unit and Integration Testing: `XUnit`, `FluentAssertions`, `Testcontainers`
  - Load Testing: `k6`, `Grafana`
  - Data seeding: `Bogus`
- **CI/CD:** `GitHub Actions`
- **Containerization:** `Docker`
- **API Documentation:** `OpenAPI (Swagger)`

## 🚀 How to start

### To clone and run this application, you'll need [Git](https://git-scm.com) and [Docker](https://www.docker.com/get-started). From your command line:

```bash
# Clone the repository
$ git clone --branch dev https://github.com/GL-Survivors/catalog-ms.git

# Navigate to the src folder
$ cd catalog-ms/src

# Create a .env file from the .env.example file
$ cp .env.example .env

# Edit the .env file to set your environment variables, if needed

# Build and run app with Docker Compose
$ docker-compose up --detach

# Go to the API
Open your browser and go to http://localhost:80 or use a REST client like Postman to access the API endpoints.

# Access the API documentation using Swagger
Open your browser and go to http://localhost:80/swagger to view and interact with the API documentation.
```

## 📡 API documentation

### 💸 Product endpoints

#### 📁 Retrieve products

```http 
GET /api/v1/Products
```
##### ***Input:***

| Query Parameter |  Type  | Required |            Valid values             |
|:----------------|:------:|:--------:|:-----------------------------------:|
| `CategoryId`    |  uuid  |    -     |                                     |
| `BrandIds`      | [uuid] |    -     |                                     |
| `MinPrice`      | double |    -     |                \> 0                 |
| `MaxPrice`      | double |    -     |         \> 0, \> `minPrice`         |
| `Query`         | string |    -     |                                     |
| `OrderBy`       | string |    -     | "FullPrice", "Discount", "Quantity" |
| `IsDesc`        |  bool  |    -     |                                     |
| `PageIndex`     |  int   |    -     |               [1 : ]                |
| `PageSize`      |  int   |    -     |              [1 : 100]              |

##### ***Output:***

- *200* array of [products](#-product-object) if found, otherwise *404*.
- *400* error code and problem details if params are invalid.

#### 📎 Retrieve product by Id

```http 
GET /api/v1/Products/{productId:guid}
```

##### ***Output:***

- *200* [product](#-product-object) if found, otherwise *404*.
- *400* problem details if params are invalid.

#### 💸 Product object
 ```json
{
  "productId": "e0c58b72-e65f-45af-ae7c-fd0f66b57535",
  "name": "Valve Corporation Game Controller",
  "description": "Lorem ipsum dolor sit amet, rebum graeco patrioque vel ut.",
  "fullPrice": 9999,
  "discount": 2499,
  "quantity": 0,
  "isActive": false,
  "images": [
    {
      "imageUrl": "blob.com/gl-survivors/proudctImage/16536a66-555a-45be-93e0-3660b3bd3383.png",
      "isMain": true
    }
  ],
  "category": {
    "categoryId": "29201f3c-ceaf-4a0e-b3a4-9df3b6b9a6b4",
    "name": "Gaming Consoles and Games",
    "image": "blob.com/gl-survivors/categories/GamingConsolesAndGames.png"
  },
  "brand": {
    "brandId": "8df72993-5c4b-4d73-95a5-8d6f4ad209c9",
    "name": "ValveCorporation",
    "image": "blob.com/gl-survivors/categories/ValveCorporation.png"
  }
}
```

___

### 🔖 Category endpoints

#### 📁 Retrieve All Categories

```http 
GET /api/v1/Categories
```

##### ***Output:***

- *200* array of [Categories](#-category-object) if found, otherwise *404*.
- *400* problem details if params are invalid.

#### 📎 Retrieve category by Id

```http 
GET /api/v2/Categories/{categoryId:guid}
```

##### ***Output:***

- *200* [Category](#-category-object) if found, otherwise *404*.
- *400* problem details if params are invalid.

#### 🔖 Category object
```json
{
  "categoryId": "9b3494c3-2a5b-4ee3-b78a-7e78a78fe0c7",
  "name": "EBooks and Peripherals",
  "image": "blob.com/gl-survivors/categories/EBooksAndPeripherals.png"
}
```

___

### 💼 Brand endpoints

#### 📁 Retrieve All Brands

```http 
GET /api/v1/Brands
```

##### ***Output:***

- *200* array of [Brands](#-brand-object) if found, otherwise *404*.
- *400* error code and problem details if params are invalid.

#### 📎 Retrieve brand by Id

```http 
GET /api/v2/Brands/{brandId:guid}
```

##### ***Output:***

- *200* [Brand](#-brand-object) if found, otherwise *404*.
- *400* error code and problem details if params are invalid.

#### 💼 Brand object
```json
{
  "brandId": "64de2e62-fb95-4abf-9171-8d903fbdd1fd",
  "name": "Sega",
  "image": "blob.com/gl-survivors/categories/Sega.png"
}
```

___

## 📮 Event contracts

### 💸 Product
#### 📃 product-created-event
| Parameter   |  Type   |
|:------------|:-------:|
| Id          |  Guid   |
| Name	       | string  |
| Description | string  |
| FullPrice   | decimal |
| Discount	   | decimal |
| Quantity	   |   int   |
| IsActive	   |  bool   |
| CategoryId  |  Guid   |
| BrandId     |  Guid   |

#### 📝 product-updated-event
| Parameter   |   Type   |
|:------------|:--------:|
| Id          |   Guid   |
| Name	       |  string  |
| Description |  string  |
| FullPrice   | decimal  |
| Discount	   | decimal  |
| Quantity	   |   int    |
| IsActive	   |   bool   |
| CategoryId  |   Guid   |
| BrandId     |   Guid   |


#### 🚫 product-deleted-event
| Parameter   |  Type   |
|:------------|:-------:|
| Id          |  Guid   |

___

### 🖼️ Product Image
#### 📃 product-image-created-event

| Parameter |  Type   |
|:----------|:-------:|
| Id        |   int   |
| ProductId |   int   |
| ImageUrl	 | string  |
| IsMain    | boolean |


#### 📝 product-image-updated-event
| Parameter |  Type   |
|:----------|:-------:|
| Id        |   int   |
| ProductId |   int   |
| ImageUrl	 | string  |
| IsMain    | boolean |

#### 🚫 product-image-deleted-event
| Parameter   |  Type   |
|:------------|:-------:|
| Id          |  Guid   |

___

### 🔖 Category
#### 📃 category-created-event
| Parameter |  Type  |
|:----------|:------:|
| Id        |  int   |
| Name	     | string |
| Image     | string |

#### 📝 category-updated-event
| Parameter |  Type  |
|:----------|:------:|
| Id        |  int   |
| Name	     | string |
| Image     | string |

#### 🚫 category-deleted-event
| Parameter   |  Type   |
|:------------|:-------:|
| Id          |  Guid   |

___

### 🛍️ Brand

#### 📃 brand-created-event
| Parameter |  Type  |
|:----------|:------:|
| Id        |  int   |
| Name	     | string |
| Image     | string |

#### 📝 brand-updated-event
| Parameter |  Type  |
|:----------|:------:|
| Id        |  int   |
| Name	     | string |
| Image     | string |

#### 🚫 brand-deleted-event
| Parameter   |  Type   |
|:------------|:-------:|
| Id          |  Guid   |
