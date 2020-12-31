*** Settings ***
Library           SeleniumLibrary

*** Variables ***
${SERVER}         localhost:8080
${BROWSER}        Chrome
${DELAY}          0.5
${MAIN URL}       http://${SERVER}
${LOGIN URL}      http://${SERVER}/login
${REGISTER URL}   http://${SERVER}/register
${WELCOME URL}    http://${SERVER}/welcome.html
${ERROR URL}      http://${SERVER}/error.htm

*** Keywords ***
Open Browser To Main Page
    Open Browser    ${MAIN URL}    ${BROWSER}
    Maximize Browser Window
    Set Selenium Speed    ${DELAY}

Go to Register Page
    Go To    ${REGISTER URL}

Go to Login Page
    Go To   ${LOGIN URL}

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