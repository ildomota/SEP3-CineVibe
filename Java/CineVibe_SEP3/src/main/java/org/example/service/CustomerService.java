package org.example.service;

import org.example.model.Customer;
import org.example.repository.CustomerRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.stereotype.Service;

import java.time.LocalDateTime;
import java.util.List;

@Service
public class CustomerService {

    @Autowired
    private CustomerRepository customerRepository;

    private final BCryptPasswordEncoder passwordEncoder = new BCryptPasswordEncoder();

    // Retrieve all customers
    public List<Customer> getAllCustomers() {
        return customerRepository.findAll();
    }

    // Retrieve a customer by ID
    public Customer getCustomerById(Integer id) {
        return customerRepository.findById(id).orElseThrow(() -> new RuntimeException("Customer not found"));
    }

    // Retrieve a customer by username
    public Customer getCustomerByUsername(String username) {
        return customerRepository.findByUsername(username);
    }

    // Create a new customer with hashed password
    public Customer createCustomer(Customer customer) {
        // Hash the password before saving it
        customer.setPasswordHash(passwordEncoder.encode(customer.getPasswordHash()));
        return customerRepository.save(customer);
    }


    // Update a customer
    public Customer updateCustomer(Integer id, Customer updatedCustomer) {
        Customer existingCustomer = getCustomerById(id);
        existingCustomer.setUsername(updatedCustomer.getUsername());
        existingCustomer.setPasswordHash(passwordEncoder.encode(updatedCustomer.getPasswordHash())); // Hash new password
        existingCustomer.setEmail(updatedCustomer.getEmail());
        existingCustomer.setStatus(updatedCustomer.getStatus());
        existingCustomer.setLoyaltyPoints(updatedCustomer.getLoyaltyPoints());
        existingCustomer.setUpdatedAt(LocalDateTime.now());
        return customerRepository.save(existingCustomer);
    }

    // Delete a customer
    public void deleteCustomer(Integer id) {
        if (customerRepository.existsById(id)) {
            customerRepository.deleteById(id);
        } else {
            throw new RuntimeException("Customer not found");
        }
    }

    // Login service
    public Customer login(String username, String password) {
        Customer customer = customerRepository.findByUsername(username);
        if (customer == null) {
            throw new RuntimeException("Invalid username or password");
        }
        if (passwordEncoder.matches(password, customer.getPasswordHash())) {
            return customer; // Login successful
        }
        throw new RuntimeException("Invalid username or password");
    }

}
