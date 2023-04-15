# Currency Exchanger 
## This app used to get current currency exchange rate 

### Features

- User can Select Base and target currency to get current exchange rate 
- app uses https://freecurrencyapi.com/ as backend to get currrenct exchange rates 
- there is two versions of this app one based on Umbraco CMS and the other based on strapi and blazor wasm as frontend 
- Strapi version allow user to save currency pairs and create view list .

### Umbraco Features
- Pages 
-- Home Page
-- Convert Result Page 
-- About Us 
- Partial Views
-- NavBar
-- Footer
-- BreadText 
- Currency Service 
-- Get Currency List 
-- Get Exchange Rate

### Strapi 
- User 
-- [StrapiURL]/api/auth/local/
-- [StrapiURL]/api/auth/local/register
- CurrencyPair 
-- POST [StrapiURL]/api/currencypairs
-- GET [StrapiURL]/api/currencypairs
-- GET [StrapiURL]/api/currencypairs/{id}
-- DELETE [StrapiURL]/api/currencypairs/{id}
-- PUT [StrapiURL]/api/currencypairs/{id}

### Strapi Blazor UI
- Pages 
-- Home Page
-- Login
-- Register
-- User Favorite Currency List
-- Convert Result Page 
-- About Us 
- Partial Views
-- NavBar
-- Footer 
- Currency Service 
-- Get Currency List 
-- Get Exchange Rate
- Strapi Service
-- Register
-- Login
-- Logout
-- GetUserCurrencyPairList
-- AddCurrencyPairToList
-- RemoveCurrencyPairFromList