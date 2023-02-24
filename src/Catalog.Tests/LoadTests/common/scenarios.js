import {fetchBrands, fetchCategories, fetchProducts} from './requests.js';
import {NEW_ITERATION_SLEEP, NEXT_PAGE_SLEEP} from './constants.js';
import {getRandomParameters} from './parameters-generator.js';
import exec from 'k6/execution';
import {Trend} from 'k6/metrics';
import {sleep} from 'k6';

export const getProductsTrend = new Trend('get_products_duration', true);
export const getBrandsTrend = new Trend('get_brands_duration', true);
export const getCategoriesTrend = new Trend('get_categories_duration', true);

export function loadTest() {
    // Fetch all categories and all brands
    const {categories, brands} = {categories: fetchCategories(), brands: fetchBrands()};

    // Retrieve random query parameters for this iteration
    const {categoryId, brandIds, ordering, priceRange, pageSize} = getRandomParameters(categories, brands);

    // Loop through each page of results until no products or the progress of the scenario is complete
    for (let pageIndex = 0; ; pageIndex++) {
        // Check if the scenario came to an end, so no extra requests would be made
        if (exec.scenario.progress === 1) break;

        const products = fetchProducts({categoryId, brandIds, pageIndex, pageSize, ordering, priceRange});

        // If the number of products is not equal to the page size,
        // it indicates that it's the last page or the page was already empty.
        // So if the statement is true -> next page, otherwise -> new iteration.
        if (products.length === pageSize) 
            sleep(NEXT_PAGE_SLEEP);
        else { 
            sleep(NEW_ITERATION_SLEEP); 
            break;
        }
    }
}