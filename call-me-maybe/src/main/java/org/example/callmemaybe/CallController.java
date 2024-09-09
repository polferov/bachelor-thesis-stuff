package org.example.callmemaybe;

import org.springframework.http.HttpHeaders;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestHeader;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class CallController {
    private final CallService callService;

    public CallController(CallService callService) {
        this.callService = callService;
    }

    @GetMapping("/call/{depth}")
    public void printDepth(@PathVariable("depth") int depth, @RequestHeader HttpHeaders headers) {
        System.out.println("Depth: " + depth);
        headers.forEach((key, value) -> System.out.println(key + ":" + value));
        if (depth > 0) {
            callService.call(depth - 1);
        }
    }
}
