import {MAX_BRANDS, ORDERBY_OPTIONS, IS_DESC_RATIO, MAX_PRICE_RANGE, PAGE_SIZES_RATIO, PARAMETERS_NULL_RATIO} from './constants.js';

export function getRandomParameters(categories, brands) {
    return {
        categoryId: generateCategoryId(categories),
        brandIds: generateBrandIds(brands),
        ordering: generateOrdering(),
        priceRange: generatePriceRange(),
        pageSize: generatePageSize()
    };
}

function generateCategoryId(categories) {
    if (parametersNullProbability()) return null;

    const randomIndex = Math.trunc(Math.random() * categories.length);

    return categories[randomIndex].categoryId;
}

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

function generateOrdering() {
    if (parametersNullProbability()) return null;

    const orderBy = ORDERBY_OPTIONS[Math.trunc(Math.random() * ORDERBY_OPTIONS.length)];
    const isDesc = Math.random() < IS_DESC_RATIO;

    return {orderBy, isDesc};
}

function generatePriceRange() {
    if (parametersNullProbability()) return null;

    const minPrice = Math.floor(Math.random() * MAX_PRICE_RANGE);
    const maxPrice = Math.floor(Math.random() * (MAX_PRICE_RANGE - minPrice)) + minPrice;

    return {minPrice, maxPrice};
}

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

function parametersNullProbability() {
    return Math.random() < PARAMETERS_NULL_RATIO;
}