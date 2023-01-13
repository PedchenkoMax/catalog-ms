<h1>Catalog microservice</h1>

<h2>Main responsibilities</h2>

<li>Retrieving and displaying product information and details, such as <b>product name</b>, <b>description</b>, <b>price</b>, <b>images</b>, and <b>availability</b>.</li>
<li>Filtering and searching for products based on various criteria, such as <b>category</b>, <b>price range</b>, and <b>keywords</b>.</li>
<li>Sorting and ordering products based on different criteria, such as <b>popularity</b>, <b>price</b>, and <b>rating</b>.</li>
<li>Providing detailed information about product reviews, ratings, and other user-generated content.</li>

<h2>API documentation</h2>

<p>This API allows users to perform read operations on products, categories, and brands. </p>
<p>The following routes and methods are available:</p>

<hr>

<h2>🛍️Products</h2>


<h3>🔍 Get all products</h3>

<strong>GET</strong> <code>/api/products</code>

<p>Retrieves all products in the system, with pagination support.</p>

<h4>Query Parameters</h4>

<ul>
  <li><code>pageSize</code>: The number of products per page. Default: 10</li>
  <li><code>pageIndex</code>: The current page number. Default: 0</li>
</ul>

<h4>Response</h4>

<ul>
  <li><strong>200 OK</strong>: Returns a paginated list of products</li>
</ul>

<br>

<h3>📝 Get product by ID</h3>

<strong>GET</strong> <code>/api/products/{productId:guid}</code>

<p>Retrieves a single product by its ID.</p>

<h4>Path Parameters</h4>

<ul>
  <li><code>productId</code>: The unique ID of the product.</li>
</ul>

<h4>Response</h4>

<ul>
  <li><strong>200 OK</strong>: Returns the product</li>
  <li><strong>404 Not Found</strong>: If the product cannot be found</li>
  <li><strong>400 Bad Request</strong>: If the product ID is not a valid Guid</li>
</ul>

<br>

<h3>📝 Get products by category and brand</h3>

<strong>GET</strong> <code>/api/products/category/{categoryId:guid}/brand/{brandId:guid}</code>

<p>Retrieves all products that match the specified category and brand.</p>

<h4>Path Parameters</h4>

<ul>
  <li><code>categoryId</code>: The unique ID of the category.</li>
  <li><code>brandId</code>: The unique ID of the brand.</li>
</ul>

<h4>Response</h4>

<ul>
  <li><strong>200 OK</strong>: Returns a list of products</li>
  <li><strong>400 Bad Request</strong>: If the category or brand ID is not a valid Guid</li>
</ul>

<br><hr>

<h2>📋 Categories</h2>

<h3>📚 Get all categories</h3>

<strong>GET</strong> <code>/api/categories</code>

<p>Retrieves all categories in the system.</p>

<h4>Response</h4>

<ul>
  <li><strong>200 OK</strong>: Returns a list of categories</li>
</ul>

<br><hr>

<h2>💼 Brands</h2>

<h3>📚 Get all brands</h3>

<strong>GET</strong> <code>/api/brands</code>

<p>Retrieves all brands in the system.</p>
<h4>Response</h4>
<ul>
  <li><strong>200 OK</strong>: Returns a list of brands</li>
</ul>

<br><hr>

<ul>
  <li><strong>Product</strong>
    <ul>
      <li><code>ProductId</code>: A Guid representing the unique id of the product</li>
      <li><code>Name</code>: A string representing the name of the product</li>
      <li><code>Quantity</code>: An int representing the quantity of the product</li>
      <li><code>Price</code>: A decimal representing the price of the product</li>
      <li><code>Image</code>: A string representing the image url of the product</li>
      <li><code>Description</code>: A string representing the description of the product</li>
      <li><code>Category</code>: A category object representing the category of the product</li>
      <li><code>Brand</code>: A brand object representing the brand of the product</li>
    </ul>
  </li>

<br>

  <li><strong>Brand</strong>
    <ul>
      <li><code>BrandId</code>: A Guid representing the unique id of the brand</li>
      <li><code>Name</code>: A string representing the name of the brand</li>
    </ul>
  </li>

<br>

  <li><strong>Category</strong>
    <ul>
      <li><code>CategoryId</code>: A Guid representing the unique id of the category</li>
      <li><code>Name</code>: A string representing the name of the category</li>
    </ul>
  </li>
</ul>

<hr>