package org.example.repository;

import jakarta.transaction.Transactional;
import org.example.model.Customer;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;

import java.util.List;

import static org.junit.jupiter.api.Assertions.*;

@SpringBootTest
@Transactional
public class CustomerRepositoryTest {

    @Autowired
    private CustomerRepository customerRepository;

    @Test
    void testFindAll() {
        customerRepository.save(new Customer( "john_does", "hashed_password_1", "john.does@example.com", "Active", 100));
        customerRepository.save(new Customer( "jane_smiths", "hashed_password_2", "jane.smiths@example.com", "Active", 200));

        List<Customer> customers = customerRepository.findAll();
        assertEquals(4, customers.size());
        assertEquals("john_doe", customers.get(0).getUsername());
    }

}
