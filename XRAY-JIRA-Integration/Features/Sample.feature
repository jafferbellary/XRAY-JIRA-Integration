Feature: Sample

@SP-1
  Scenario: Add Two Number
    Given I have entered 100 and 25 into the calculator
    When I do addition of two number
    Then the result should be 125 on the screen

  @SP-2
  Scenario: Subtract Two Number
    Given I have entered 100 and 25 into the calculator
    When I do subtraction of two number
    Then the result should be 75 on the screen

  @SP-3
  Scenario: Multiply Two Number
    Given I have entered 5 and 5 into the calculator
    When I do multiply of two number
    Then the result should be 25 on the screen

  @SP-4
  Scenario: Divide Two Number
    Given I have entered 100 and 25 into the calculator
    When I do divide of two number
    Then the result should be 4 on the screen

  @SP-5
  Scenario: Mod of Two Number
    Given I have entered 100 and 24 into the calculator
    When I do mod of two number
    Then the result should be 3 on the screen
