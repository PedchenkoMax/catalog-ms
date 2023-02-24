import {ENDPOINT_PRODUCTS, ENDPOINT_CATEGORIES, ENDPOINT_BRANDS} from './constants.js';
import {getProductsTrend, getCategoriesTrend, getBrandsTrend} from './scenarios.js';
import {URL} from 'https://jslib.k6.io/url/1.0.0/index.js';
import http from 'k6/http';

export function fetchProducts({categoryId, brandIds, pageIndex, pageSize, ordering, priceRange}) {
    const url = new URL(ENDPOINT_PRODUCTS);

    if (categoryId !== null)
        url.searchParams.append('categoryId', categoryId);
    if (brandIds !== null)
        for (const brandId of brandIds)
            url.searchParams.append('brandIds', brandId);

    if (pageIndex !== null)
        url.searchParams.append('pageIndex', pageIndex);
    if (pageSize !== null)
        url.searchParams.append('pageSize', pageSize);

    if (priceRange !== null) {
        url.searchParams.append('minPrice', priceRange.minPrice);
        url.searchParams.append('maxPrice', priceRange.maxPrice);
    }
    if (ordering !== null) {
        url.searchParams.append('orderBy', ordering.orderBy);
        url.searchParams.append('isDesc', ordering.isDesc);
    }

    const products = http.get(url.toString(), {responseCallback: http.expectedStatuses(200, 404)});

    getProductsTrend.add(products.timings.duration);

    return products.json();
}

export function fetchCategories() {

    const categories = http.get(ENDPOINT_CATEGORIES);

    getCategoriesTrend.add(categories.timings.duration);

    return categories.json();
}

export function fetchBrands() {
    const brands = http.get(ENDPOINT_BRANDS);

    getBrandsTrend.add(brands.timings.duration);

    return brands.json();
}