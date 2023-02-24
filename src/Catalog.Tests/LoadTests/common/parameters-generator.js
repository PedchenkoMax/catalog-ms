import {MAX_BRANDS, ORDERBY_OPTIONS, IS_DESC_RATIO, MAX_PRICE_RANGE, PAGE_SIZES_RATIO, PARAMETERS_NULL_RATIO} from './constants.js';

/**
 * Generates query parameters with PARAMETERS_NULL_RATIO chance for each to be null.
 */
export function getRandomParameters(categories, brands) {
    return {
        categoryId: generateCategoryId(categories),
        brandIds: generateBrandIds(brands),
        ordering: generateOrdering(),
        priceRange: generatePriceRange(),
        pageSize: generatePageSize()
    };
}

/**
 * Obtains a random ID from categories.
 */
function generateCategoryId(categories) {
    if (parametersNullProbability()) return null;

    const randomIndex = Math.trunc(Math.random() * categories.length);

    return categories[randomIndex].categoryId;
}

/**
 * Obtains a random list of IDs from brands, up to a maximum of MAX_BRANDS.
 */
function generateBrandIds(brands) {
    if (parametersNullProbability()) return null;

    const numBrands = Math.ceil(Math.random() * MAX_BRANDS);

    const brandIds = [];

    while (brandIds.length < numBrands) {
        const randomBrandId = brands[Math.trunc(Math.random() * brands.length)].brandId;

        if (!brandIds.includes(randomBrandId)) brandIds.push(randomBrandId);
    }

    return brandIds;
}

/**
 * Obtains random orderBy from ORDERBY_OPTIONS
 * and generates isDesc on ratio provided by IS_DESC_RATIO.
 */
function generateOrdering() {
    if (parametersNullProbability()) return null;

    const orderBy = ORDERBY_OPTIONS[Math.trunc(Math.random() * ORDERBY_OPTIONS.length)];
    const isDesc = Math.random() < IS_DESC_RATIO;

    return {orderBy, isDesc};
}

/**
 * Generates a random valid priceRange in MAX_PRICE_RANGE.
 */
function generatePriceRange() {
    if (parametersNullProbability()) return null;

    const minPrice = Math.floor(Math.random() * MAX_PRICE_RANGE);
    const maxPrice = Math.floor(Math.random() * (MAX_PRICE_RANGE - minPrice)) + minPrice;

    return {minPrice, maxPrice};
}

/**
 *  Obtains a random page size based on PAGE_SIZES_RATIO.
 */
function generatePageSize() {
    const rand = Math.random();

    let cumulativeRatio = 0;
    for (const [pageSize, ratio] of Object.entries(PAGE_SIZES_RATIO)) {
        cumulativeRatio += ratio;
        if (rand <= cumulativeRatio)
            return parseInt(pageSize);
    }

    return 25;
}

/**
 * Returns true with a chance determined by PARAMETERS_NULL_RATIO,
 * indicating whether a function parameter should be set to null or not.
 */
function parametersNullProbability() {
    return Math.random() < PARAMETERS_NULL_RATIO;
}