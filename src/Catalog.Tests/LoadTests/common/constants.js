export const HOST_URL = 'http://localhost:5050';
export const ENDPOINT_PRODUCTS = `${HOST_URL}/api/Products`;
export const ENDPOINT_CATEGORIES = `${HOST_URL}/api/Categories`;
export const ENDPOINT_BRANDS = `${HOST_URL}/api/Brands`;

export const NEXT_PAGE_SLEEP = 1;
export const NEW_ITERATION_SLEEP = 5;

export const MAX_BRANDS = 10;
export const ORDERBY_OPTIONS = ['FullPrice', 'Discount', 'Quantity'];
export const IS_DESC_RATIO = 0.5;
export const MAX_PRICE_RANGE = 15000;
export const PAGE_SIZES_RATIO = {25: 0.5, 50: 0.2, 100: 0.3}; // weights must equal to 1
export const PARAMETERS_NULL_RATIO = 0.3;