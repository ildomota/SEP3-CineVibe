package org.example.controller;

import org.example.Main;
import org.example.model.Customer;
import org.example.repository.CustomerRepository;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.test.web.server.LocalServerPort;
import org.springframework.http.ResponseEntity;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.web.client.RestTemplate;

import static org.junit.jupiter.api.Assertions.assertEquals;

@SpringBootTest(webEnvironment = SpringBootTest.WebEnvironment.RANDOM_PORT, classes = Main.class)
class CustomerControllerTest {

    @LocalServerPort
    private int port;

    @Autowired
    private CustomerRepository customerRepository;

    @Autowired
    private JdbcTemplate jdbcTemplate;

    private RestTemplate restTemplate;

    @BeforeEach
    void setup() {
        // Clear dependent tables first
        jdbcTemplate.execute("TRUNCATE TABLE cinevibe.moviereviews RESTART IDENTITY CASCADE");
        jdbcTemplate.execute("TRUNCATE TABLE cinevibe.bookings RESTART IDENTITY CASCADE");
        jdbcTemplate.execute("TRUNCATE TABLE cinevibe.snackorders RESTART IDENTITY CASCADE");
        jdbcTemplate.execute("TRUNCATE TABLE cinevibe.loyaltypoints RESTART IDENTITY CASCADE");

        // Clear the customers table
        customerRepository.deleteAll();

        // Insert test customers
        customerRepository.save(new Customer( "john_doe", "hashed_password_1", "john.doe@example.com", "Active", 100));
        customerRepository.save(new Customer( "jane_smith", "hashed_password_2", "jane.smith@example.com", "Active", 200));

        restTemplate = new RestTemplate();
    }

    @Test
    void testGetAllCustomers() {
        String url = "http://localhost:" + port + "/api/customers";
        ResponseEntity<Customer[]> response = restTemplate.getForEntity(url, Customer[].class);

        assertEquals(200, response.getStatusCodeValue());
        Customer[] customers = response.getBody();
        assertEquals(2, customers.length);
        assertEquals("john_doe", customers[0].getUsername());
        assertEquals("jane_smith", customers[1].getUsername());
    }
}
