package org.example.callmemaybe;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;
import org.springframework.web.client.RestTemplate;

@Service
public class CallService {
    @Value("${external.service.url}")
    private String externalServiceUrl;

    private final RestTemplate restTemplate;

    public CallService(RestTemplate restTemplate) {
        this.restTemplate = restTemplate;
    }

    public void call(int depth) {
        if (depth < 0) {
            throw new IllegalArgumentException("Depth must be greater than 0");
        }
        String url = externalServiceUrl + "/call/" + depth;
        restTemplate.getForObject(url, Void.class);
    }
}
