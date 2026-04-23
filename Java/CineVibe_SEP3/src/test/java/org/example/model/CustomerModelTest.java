package org.example.model;

import org.junit.jupiter.api.Test;

import java.time.LocalDateTime;

import static org.junit.jupiter.api.Assertions.*;

public class CustomerModelTest {

    @Test
    public void testCustomerModel() {
        LocalDateTime now = LocalDateTime.now();
        Customer customer = new Customer( "john_doe", "hashed_password_1", "john.doe@example.com", "Active", 100);


        assertEquals("john_doe", customer.getUsername());
        assertEquals("hashed_password_1", customer.getPasswordHash());
        assertEquals("john.doe@example.com", customer.getEmail());
        assertEquals("Active", customer.getStatus());
        assertEquals(100, customer.getLoyaltyPoints());
    }
}
