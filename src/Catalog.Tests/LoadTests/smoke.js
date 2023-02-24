// Smoke Load Testing
// This type of testing is done to quickly verify if the system is working as expected under a light load.
// Total: 3m

import {loadTest} from './common/scenarios.js';

export const options = {
    stages: [
        {duration: '15s', target: 10},
        {duration: '45s', target: 25},
        {duration: '1m', target: 50},
        {duration: '45s', target: 25},
        {duration: '15s', target: 0}
    ],
    thresholds: {
        http_req_failed: ['rate < 0.001'], // 0.1%
        http_req_duration: ['p(95) < 150'],

        get_products_duration: ['p(95) < 75'],
        get_brands_duration: ['p(95) < 25'],
        get_categories_duration: ['p(95) < 25']
    }
};

export default loadTest;