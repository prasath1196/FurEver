# FurEver  
### Welcome to FurEver – the online haven built for the community of dog lovers, forever!  
Website URL: https://furever.azurewebsites.net/

Design Document  

Prasath Rajasekaran  
Priyanka Somireddi  
Sahith Bandi  
Vivek Maddukuri

## Introduction

Finding the perfect dog breed can be an exciting journey, especially when considering which breed best fits your lifestyle and how to care for your new companion. **FurEver** is here to help:

- Browse through a variety of dog breeds to find the ones that catch your eye or that you’d like to explore more.
- Share and read stories about your dog’s journey, from playful moments to heartwarming memories, with a community of fellow dog lovers.
- Discover top pet products recommended by brand, helping you find quality options for all your dog’s needs.
- Access detailed views of each dog breed, including physical traits, personality, friendliness, and care essentials.
- Use our Dog Match Quiz to find a breed that suits your personality and lifestyle, making the adoption process easier and more informed.

## Brand Logo 
![Updated Logo FurEver](https://github.com/user-attachments/assets/a7617d5e-f980-4e57-a95c-c454390080fc)


## Integrations
- Information about dog and dog breeds -  https://registry.dog/api/v1
- Products API - https://www.samsclub.com/api/node/vivaldi/browse/v2/products/search?sourceType=1&limit=45&clubId=&searchTerm=dog+toys&br=true&secondaryResults=2&wmsponsored=1&wmsba=true&banner=true&wmVideo=true

## Functional Requirements

### Requirement 1.0: User Authentication

#### Scenario

- *As a user*, I want to securely log in or sign up with my email and password so that I can access personalized features.

#### Dependencies  
- A database for storing and retrieving user credentials.

#### Assumptions  
- Users will have unique email addresses.
- Error messages provide clear guidance for failed login or sign-up attempts.

#### Examples

  *1.1*
  
  **Given** a new user with the email newuser@example.com is on the sign-up page  
  
  **When** they enter newuser@example.com as their email and Password123! as their password and click "Sign Up"  
  
  **Then** they should be redirected to the home page and greeted with a welcome message: "Welcome, newuser@example.com!"

  *1.2*
  
  **Given** a returning user testuser@example.com is on the login page
  
  **When** they enter testuser@example.com as the email and IncorrectPass123 as the password and click "Log In"
  
  **Then** they should remain on the login page and see an error message: "Incorrect username or password. Please try again."

  *1.3*
  
  **Given** the user is not logged in
  
  **When** they try to access a restricted page, like the “Add Story” page
  
  **Then** they should be redirected to the login page with a prompt message: “Please log in to access this feature.”

### Requirement 2.0: Breed Listing and Search

#### Scenario

- *As a user interested in dog breeds*, I want to view a list of breeds and search for specific ones by name or category so that I can find breeds matching my interests.

#### Dependencies  
- A searchable breed with categories and filters for classification.

#### Assumptions  
- The breed list includes popular dog breeds with detailed descriptions.
- Users can filter breeds by groups, categories, or attributes.

#### Examples

  *2.1*
  
  **Given** a list of breeds is available and includes “Golden Retriever” and “Labrador Retriever”
  
  **When** the user selects the filter for “Companion” group
  
  **Then** the list should display breeds in the Companion group, including “Golden Retriever” and “Labrador Retriever”

  *2.2*
  
  **Given** the breed list includes “Labrador Retriever”
  
  **When** the user enters "Labrador" in the search bar and clicks "Search"
  
  **Then** the search results should include “Labrador Retriever” with attributes such as “Friendly, Medium to Large size, High energy”

  *2.3*
  
  **Given** the breed list is available
  
  **When** the user enters an invalid search term like "xyzBreed" and clicks "Search"
  
  **Then** they should see an empty list with a message: “No results found for ‘xyzBreed’.”

### Requirement 3.0: Breed Profile

#### Scenario

- *As a user*, I want to view detailed profiles for each breed, including a description, physical traits, friendliness rating, and care requirements, so that I can learn about each breed in depth.

#### Dependencies  
- Detailed breed profiles with standardized information.

#### Assumptions  
- Breed data is accurate and updated regularly.
- Users can view breed profiles without logging in.

#### Examples

  *3.1*
  
  **Given** the user is viewing the breed profile for "Golden Retriever"
  
  **When** they navigate to the profile page
  
  **Then** they should see detailed information, including "Friendly, adaptable, loves children," size range "55-75 lbs," and care requirements like "High exercise needs"

### Requirement 4.0: Pet Stories

#### Scenario

- *As an authenticated user*, I want to read and contribute pet stories under each breed so that I can share experiences with other users.

#### Dependencies  
- Detailed breed profiles with standardized information.

#### Assumptions  
- Users have a positive experience sharing and viewing stories.

#### Examples

  *4.1*
  
  **Given** the user is logged in and viewing the profile for “Beagle”
  
  **When** they click “Add Story” and submit a story titled “Life with Bailey” with content “Bailey loves to run around in the park!”
  
  **Then** the new story should appear under the “Beagle” profile with the title “Life with Bailey” and content “Bailey loves to run around in the park!”

  *4.2*
  
  **Given** there is a story posted by another user under the breed profile for “Beagle”
  
  **When** the user views the Beagle profile page
  
  **Then** they should see the stories section displaying all existing stories, including the recently added one

### Requirement 5.0: Dog Match Quiz

#### Scenario

- *As a user*, I want to take a quiz that recommends dog breeds based on my preferences and lifestyle so that I can find the best match for my needs.

#### Dependencies  
- Interactive quiz questions to find the best match.

#### Assumptions  
- Quiz questions are relevant to matching dog breeds with user lifestyles.
- Recommendations are accurate and helpful for the user’s needs.

#### Examples

  *5.1*  
  
  **Given** the quiz is available with questions on activity level, grooming needs, and friendliness
  
  **When** the user selects “High activity,” “Low grooming needs,” and “Family-friendly”
  
  **Then** the app should recommend breeds such as “Labrador Retriever” and “Beagle” based on the responses

  *5.2*
  
  **Given** the user has started the quiz
  
  **When** they leave a question unanswered and attempt to submit the quiz
  
  **Then** they should see an error message prompting them to answer all questions: “Please answer all questions to see your dog match results.”

### Requirement 6.0: Top Pet Products

#### Scenario

- *As a user*, I want to view top-rated pet products from well-known brands that are tailored for my dog's breed so that I can find the best items for my pet’s needs.

#### Dependencies  
- Links to product details and purchase options.

#### Assumptions  
- Product data is relevant, updated, and sourced from reputable brands.

#### Examples

  *6.1*
  
  **Given** a user is viewing the breed profile for “Golden Retriever”
  
  **When** they navigate to the “Top Products” section
  
  **Then** they should see a curated list of products specific to Golden Retrievers, including options for food, grooming items, and toys from brands such as “Purina,” “Kong,” and “Blue Buffalo”

  *6.2*
  
  **Given** the user is on the “Top Products” section of the Golden Retriever profile
  
  **When** they click on a product link
  
  **Then** they should be redirected to the product’s detailed page for more information or purchase options


## Scrum Roles

- DevOps/Product Owner/Scrum Master: Vivek, Prasath
- Frontend Developers: Prasath, Priyanka, Sahith
- Integration Developers: Priyanka, Sahith, Vivek

## Weekly Meeting

Every Tuesday at 6.00 p.m.
