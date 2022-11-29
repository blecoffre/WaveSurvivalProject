
using Cysharp.Threading.Tasks;
using System.Linq;
using UniRx;
using UnityEngine;

public class BaseTurret : MonoBehaviour, IDamageable<float>, IAttack
{
    [SerializeField] private float radius = 5.0f;
    [SerializeField] private float depth = 10.0f;
    [SerializeField] private float angle = 33.0f;

    [SerializeField] private float _rotationSpeed = 25.0f;

    [SerializeField] private Transform _rotativeBase = default;
    [SerializeField] private Transform _muzzleNoze = default;

    private Transform _currentEnemyTr = default;
    private Vector3 _shootTargetPosition = default;

    private void Start()
    {
        Observable.EveryUpdate().Where(_ => _currentEnemyTr == null).Subscribe(async _ =>
        {
            Vector3 prevTargetPos = _currentEnemyTr.position;
            await UniTask.DelayFrame(1);
            Vector3 targetSpeed = (_currentEnemyTr.position - prevTargetPos) / Time.deltaTime;
            _shootTargetPosition = Vector3Utils.PredictV3Pos(_muzzleNoze.position, 10, _currentEnemyTr.position, targetSpeed, false);
        });

        Observable.EveryUpdate().Where(_ => _currentEnemyTr != null).Subscribe(_ =>
        {
            LookAtTarget();
        });
    }

    public void TakeDamage(float damageTaken)
    {

    }

    public void Attack()
    {
        Vector3 dir = _currentEnemyTr.position - _rotativeBase.position;
        dir = _currentEnemyTr.InverseTransformDirection(dir);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if (angle < 10.0f)
        {

        }
    }

    public void StopAttack()
    {

    }

    private Transform CheckForEnemeyInRange()
    {
        RaycastHit[] coneHits = PhysicsExtension.ConeCastAll(transform.position, radius, transform.forward, depth, angle);

        coneHits.OrderBy(x => Vector3.Distance(_rotativeBase.position, x.transform.position));
        Transform target = coneHits.First(x => x.transform.tag == "Hostile_AI").transform;

        return target;
    }

    private void LookAtTarget()
    {
        var targetRotation = Quaternion.LookRotation(_currentEnemyTr.position - _rotativeBase.position);
        var str = Mathf.Min(_rotationSpeed * Time.deltaTime, 1);
        _rotativeBase.rotation = Quaternion.Lerp(_rotativeBase.rotation, targetRotation, str);
    }
}
