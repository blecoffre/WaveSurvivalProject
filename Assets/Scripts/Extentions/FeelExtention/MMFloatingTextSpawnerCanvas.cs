using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MMFloatingTextSpawnerCanvas : MMFloatingTextSpawner
{
    protected override void InstantiateSimplePool()
    {
        if (PooledSimpleMMFloatingText == null)
        {
            Debug.LogError(this.name + " : no PooledSimpleMMFloatingText prefab has been set.");
            return;
        }
        GameObject newPooler = new GameObject();
        SceneManager.MoveGameObjectToScene(newPooler, this.gameObject.scene);
        newPooler.name = PooledSimpleMMFloatingText.name + "_Pooler";
        newPooler.transform.localPosition = transform.localPosition;
        newPooler.transform.SetParent(this.transform);
        MMSimpleObjectPooler simplePooler = newPooler.AddComponent<MMSimpleObjectPooler>();
        simplePooler.PoolSize = PoolSize;
        simplePooler.GameObjectToPool = PooledSimpleMMFloatingText.gameObject;
        simplePooler.NestWaitingPool = NestWaitingPool;
        simplePooler.MutualizeWaitingPools = MutualizeWaitingPools;
        simplePooler.PoolCanExpand = PoolCanExpand;
        simplePooler.FillObjectPool();
        _pooler = simplePooler;
        _pooler.transform.position = transform.position;
    }

    protected override void Spawn(string value, Vector3 position, Vector3 direction, float intensity = 1, bool forceLifetime = false, float lifetime = 1, bool forceColor = false, Gradient animateColorGradient = null)
    {
        if (!CanSpawn)
        {
            return;
        }

        _direction = (direction != Vector3.zero) ? direction + this.transform.up : this.transform.up;

        this.transform.position = position;

        GameObject nextGameObject = _pooler.GetPooledGameObject();

        float lifetimeMultiplier = IntensityImpactsLifetime ? intensity * IntensityLifetimeMultiplier : 1f;
        float movementMultiplier = IntensityImpactsMovement ? intensity * IntensityMovementMultiplier : 1f;
        float scaleMultiplier = IntensityImpactsScale ? intensity * IntensityScaleMultiplier : 1f;

        _lifetime = UnityEngine.Random.Range(Lifetime.x, Lifetime.y) * lifetimeMultiplier;
        _spawnOffset = MMMaths.RandomVector3(SpawnOffsetMin, SpawnOffsetMax);
        _animateColor = AnimateColor;
        _colorGradient = AnimateColorGradient;

        float remapXZero = UnityEngine.Random.Range(RemapXZero.x, RemapXZero.y);
        float remapXOne = UnityEngine.Random.Range(RemapXOne.x, RemapXOne.y) * movementMultiplier;
        float remapYZero = UnityEngine.Random.Range(RemapYZero.x, RemapYZero.y);
        float remapYOne = UnityEngine.Random.Range(RemapYOne.x, RemapYOne.y) * movementMultiplier;
        float remapZZero = UnityEngine.Random.Range(RemapZZero.x, RemapZZero.y);
        float remapZOne = UnityEngine.Random.Range(RemapZOne.x, RemapZOne.y) * movementMultiplier;
        float remapOpacityZero = UnityEngine.Random.Range(RemapOpacityZero.x, RemapOpacityZero.y);
        float remapOpacityOne = UnityEngine.Random.Range(RemapOpacityOne.x, RemapOpacityOne.y);
        float remapScaleZero = UnityEngine.Random.Range(RemapScaleZero.x, RemapOpacityZero.y);
        float remapScaleOne = UnityEngine.Random.Range(RemapScaleOne.x, RemapScaleOne.y) * scaleMultiplier;

        if (forceLifetime)
        {
            _lifetime = lifetime;
        }

        if (forceColor)
        {
            _animateColor = true;
            _colorGradient = animateColorGradient;
        }

        // mandatory checks
        if (nextGameObject == null) { return; }

        // we activate the object
        nextGameObject.gameObject.SetActive(true);
        nextGameObject.gameObject.MMGetComponentNoAlloc<MMPoolableObject>().TriggerOnSpawnComplete();

        // we position the object
        nextGameObject.transform.position = transform.position;
        nextGameObject.transform.SetParent(transform);

        _floatingText = nextGameObject.MMGetComponentNoAlloc<MMFloatingText>();
        _floatingText.SetUseUnscaledTime(UseUnscaledTime, true);
        _floatingText.ResetPosition();
        _floatingText.SetProperties(value, _lifetime, _direction, AnimateMovement,
            AlignmentMode, FixedAlignment, AlwaysFaceCamera, TargetCamera,
            AnimateX, AnimateXCurve, remapXZero, remapXOne,
            AnimateY, AnimateYCurve, remapYZero, remapYOne,
            AnimateZ, AnimateZCurve, remapZZero, remapZOne,
            AnimateOpacity, AnimateOpacityCurve, remapOpacityZero, remapOpacityOne,
            AnimateScale, AnimateScaleCurve, remapScaleZero, remapScaleOne,
            _animateColor, _colorGradient);
    }
}
