package org.example.controller;

import org.example.model.Admin;
import org.example.repository.AdminRepository;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.test.web.server.LocalServerPort;
import org.springframework.http.ResponseEntity;
import org.springframework.web.client.RestTemplate;

import java.util.List;

import static org.junit.jupiter.api.Assertions.assertEquals;

@SpringBootTest(webEnvironment = SpringBootTest.WebEnvironment.RANDOM_PORT)
class AdminControllerTest {

    @LocalServerPort
    private int port;

    @Autowired
    private AdminRepository adminRepository;

    private RestTemplate restTemplate;


    @BeforeEach
    void setup() {
        adminRepository.deleteAll();
        adminRepository.save(new Admin("admin_user", "admin_password", "admin@example.com", "Active"));
        adminRepository.save(new Admin("super_admin", "super_password", "super.admin@example.com", "Active"));
        restTemplate = new RestTemplate();
    }

    @Test
    void testGetAllAdmins() {
        String url = "http://localhost:" + port + "/api/admins";
        ResponseEntity<Admin[]> response = restTemplate.getForEntity(url, Admin[].class);

        assertEquals(200, response.getStatusCodeValue());
        Admin[] admins = response.getBody();
        assertEquals(2, admins.length);
        assertEquals("admin_user", admins[0].getUsername());

        System.out.println(url);
    }


}
