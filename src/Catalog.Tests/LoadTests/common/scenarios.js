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
    const {categories, brands} = {categories: fetchCategories(), brands: fetchBrands()};

    const {categoryId, brandIds, ordering, priceRange, pageSize} = getRandomParameters(categories, brands);

    for (let pageIndex = 0; ; pageIndex++) {
        if (exec.scenario.progress === 1) break;

        const products = fetchProducts({categoryId, brandIds, pageIndex, pageSize, ordering, priceRange});

        if (products.length === pageSize) 
            sleep(NEXT_PAGE_SLEEP);
        else { 
            sleep(NEW_ITERATION_SLEEP); 
            break;
        }
    }
}