﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    private GameManager _gameManager;

    [SerializeField] private InputField _username;
    [SerializeField] private InputField _password;

    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    public void OnLoginButtonClicked ()
    {
        var _database = _gameManager.database;

        if (_database.FindElement(_database.accounts, _username.text) != null) {
            var _account = (Account)_database.FindElement(_database.accounts, _username.text);
            if (_account.username == _username.text && _account.password == _password.text)
                print("Logging in"); //TODO: Implement
        } else StartCoroutine(Error());
    }

    public void OnSignUpButtonClicked()
    {
        var _database = _gameManager.database;
        var _account = new Account();

        _account.password = _password.text;
        _account.Id = _username.text;
        _account.username = _username.text;

        if (_database.FindElement(_database.accounts, _username.text) == null) {
            _database.accounts.Add(_account);
            _database.AddElement<Account>(_account);
        }
        else StartCoroutine(Error());
    }

    private IEnumerator Error()
    {
        _username.text = "";
        _password.text = "";

        _username.interactable = false;
        _password.interactable = false;

        yield return new WaitForSeconds(2f);

        _username.interactable = true;
        _password.interactable = true;
    }
}
