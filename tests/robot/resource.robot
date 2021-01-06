*** Settings ***
Library           Selenium2Library

*** Variables ***
${SERVER}         localhost:8080
${BROWSER}        Chrome
${DELAY}          0.5
${MAIN URL}       http://${SERVER}
${LOGIN URL}      http://${SERVER}/login
${REGISTER URL}   http://${SERVER}/register
${GROUP URL}      http://${SERVER}/home/groups
${WELCOME URL}    http://${SERVER}/welcome.html
${ERROR URL}      http://${SERVER}/error.htm
${headers}       Create Dictionary  Authorization=“Bearer eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjVmZjRhZjA1NDk1MTYyNGJjY2JjNjk3NSIsImdyb3VwcyI6IjVmZjRiODQ0NDk1MTYyNGJjY2JjNjk3NiIsImV4cCI6MTc4OTg4MDIzMCwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdCIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3QifQ.P7IpPfOqCAGelP8GVjWdr9f758Xanifj7kbByPTUYEE"

*** Keywords ***
Open Browser To Main Page
    Open Browser    ${MAIN URL}    ${BROWSER}
    Maximize Browser Window
    Set Selenium Speed    ${DELAY}

Go to Register Page
    Go To    ${REGISTER URL}

Go to Login Page
    Go To   ${LOGIN URL}

Go to Group Page
    Go To   ${GROUP URL}

Input Username
    [Arguments]    ${username}
    Input Text     101   ${username}    True

Input Password
    [Arguments]    ${password}
    Input Text     102    ${password}   True

Input Password Confirmation
    [Arguments]    ${password}
    Input Text     103    ${password}   True

Input Login
    [Arguments]     ${login}
    Input Text      100   ${login}      True

Submit Credentials
    Click Button    200

Click movie rate
    [Arguments]     ${id}   ${xoffset}
    Click Element At Coordinates    ${id}   ${xoffset}    0

Get screenshot
    [Arguments]     ${id}
    Capture Element Screenshot      ${id}

Click allergy

    [Arguments]     ${id}   ${xoffset}
    Click Element At Coordinates    ${id}   ${xoffset}    0

Click create group
    Click button   enterGroupCreation

Input group name
    [Arguments]    ${groupName}
    Input Text     groupName   ${groupName}    True

Input group descritpion
    [Arguments]    ${groupDescription}
    Input Text     groupDescription   ${groupDescription}    True

Select icon
    Click element   fas fa-bicycle

Click members list
    Click Element       //[@class="v-select__selections"]
Select group member
    [Arguments]     ${id}   ${xoffset}
    Click Element At Coordinates    ${id}   ${xoffset}    0
Create group
    Click button    createGroup