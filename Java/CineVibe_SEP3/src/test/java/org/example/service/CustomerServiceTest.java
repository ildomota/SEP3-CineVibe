package org.example.service;

import org.example.model.Customer;
import org.example.repository.CustomerRepository;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;

import java.util.List;

import static org.junit.jupiter.api.Assertions.*;

@SpringBootTest
class CustomerServiceTest {

    @Autowired
    private CustomerService customerService;

    @Autowired
    private CustomerRepository customerRepository;

    @BeforeEach
    void setup() {
        customerRepository.deleteAll();
        customerRepository.save(new Customer("john_doe", "hashed_password_1", "john.doe@example.com", "Active", 100));
        customerRepository.save(new Customer("jane_smith", "hashed_password_2", "jane.smith@example.com", "Active", 200));
    }

    @Test
    void testGetAllCustomers() {
        List<Customer> customers = customerService.getAllCustomers();
        assertEquals(2, customers.size());
        assertEquals("john_doe", customers.get(0).getUsername());
        assertEquals("jane_smith", customers.get(1).getUsername());
    }

    @Test
    void testCreateCustomer() {
        Customer newCustomer = new Customer("new_user", "new_password", "new.user@example.com", "Active", 50);
        Customer savedCustomer = customerService.createCustomer(newCustomer);
        assertEquals("new_user", savedCustomer.getUsername());
    }

    @Test
    void testLoginSuccess() {
        // Verify login with correct credentials
        Customer customer = customerService.login("john_doe", "hashed_password_1");
        assertNotNull(customer);
        assertEquals("john_doe", customer.getUsername());
    }

    @Test
    void testLoginInvalidPassword() {
        // Verify login with wrong password
        RuntimeException exception = assertThrows(RuntimeException.class, () -> {
            customerService.login("john_doe", "wrong_password");
        });
        assertEquals("Invalid username or password", exception.getMessage());
    }

    @Test
    void testLoginUserNotFound() {
        // Verify login with non-existent user
        RuntimeException exception = assertThrows(RuntimeException.class, () -> {
            customerService.login("non_existent_user", "hashed_password_1");
        });
        assertEquals("Invalid username or password", exception.getMessage());
    }
}
