// Average Load Testing
// This type of testing is done to simulate an average level of user traffic on the system.
// Total: 10m

import {loadTest} from './common/scenarios.js';

export const options = {
    stages: [
        {duration: '1.5m', target: 100},
        {duration: '2m', target: 200},
        {duration: '3m', target: 300},
        {duration: '2m', target: 100},
        {duration: '1.5m', target: 0}
    ],
    thresholds: {
        http_req_failed: ['rate < 0.01'], // 1%
        http_req_duration: ['p(95) < 750'],

        get_products_duration: ['p(95) < 500'],
        get_brands_duration: ['p(95) < 75'],
        get_categories_duration: ['p(95) < 75']
    }
};

export default loadTest;