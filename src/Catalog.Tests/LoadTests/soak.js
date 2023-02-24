// Soak Load Testing
// This type of testing is done to verify if the system can handle a sustained load over an extended period of time.
// Total: 1h

import {loadTest} from './common/scenarios.js';

export const options = {
    stages: [
        {duration: '5m', target: 200},
        {duration: '50m', target: 200},
        {duration: '5m', target: 0}
    ],
    thresholds: {
        http_req_failed: ['rate < 0.01'], // 1%
        http_req_duration: ['p(95) < 750'],

        get_products_duration: ['p(95) < 500'],
        get_brands_duration: ['p(95) < 100'],
        get_categories_duration: ['p(95) < 100']
    }
};

export default loadTest;