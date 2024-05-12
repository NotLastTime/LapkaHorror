using System;
using System.Collections;
using System.Collections.Generic;
using Services;
using UnityEngine;

/**
 * Единая точка входа для управления персонажем
 * Ввод пользователя распределяется между различными сервисами которые и будут заниматься обработкой
 * Первично обрабатывает ввод для распределения между сервисами
 */
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _cameraTransform;

    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _mouseSensitivity;
    
    //Отвечает за передвижение и поворот пользователя
    private PlayerMovementService _playerMovementService;

    //Как только игровой объект на который навешен этот скрипт становится активен, исполняется этот метод
    private void OnEnable()
    {
        _playerMovementService = new PlayerMovementService(_playerTransform, _cameraTransform);
    }

    /*
     * Вызывается каждый шаг обновления физики
     * Все действия связанные с физикой должны обрабатываться тут
     * Пример: передвижение, вращения пользователя
     */
    private void FixedUpdate()
    {
        _playerMovementService.Rotate(_mouseSensitivity);
        _playerMovementService.Move(_playerSpeed);
    }
}
