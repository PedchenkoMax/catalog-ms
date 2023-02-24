// Spike Load Testing
// This type of testing is done to simulate sudden spikes in traffic on the system.
// Total: 5m

import {loadTest} from './common/scenarios.js';

export const options = {
    stages: [
        {duration: '15s', target: 100},
        {duration: '2m', target: 750},
        {duration: '30s', target: 100},
        {duration: '2m', target: 750},
        {duration: '15s', target: 0}
    ],
    thresholds: {
        http_req_failed: ['rate < 0.03'], // 1%
        http_req_duration: ['p(95) < 1500'],

        get_products_duration: ['p(95) < 1500'],
        get_brands_duration: ['p(95) < 1000'],
        get_categories_duration: ['p(95) < 1000']
    }
};

export default loadTest;