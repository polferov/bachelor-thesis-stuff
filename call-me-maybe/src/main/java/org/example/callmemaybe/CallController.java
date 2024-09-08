package org.example.callmemaybe;

import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class CallController {
    private final CallService callService;

    public CallController(CallService callService) {
        this.callService = callService;
    }

    @GetMapping("/call/{depth}")
    public void printDepth(@PathVariable("depth") int depth) {
        System.out.println("Depth: " + depth);
        if (depth > 0) {
            callService.call(depth - 1);
        }
    }
}
