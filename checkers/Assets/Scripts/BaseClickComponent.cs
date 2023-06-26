using System.Linq;

using UnityEngine;
using UnityEngine.EventSystems;

namespace Checkers
{
    public abstract class BaseClickComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public Material focusMaterial;
        //Меш игрового объекта
        public MeshRenderer mesh;
        //Список материалов на меше объекта
        private Material[] _meshMaterials = new Material[3];

        [Tooltip("Цветовая сторона игрового объекта"), SerializeField]
       public ColorType Color { get; set; }


       
        public void AddAdditionalMaterial(Material material, int index = 1)
        {
           
            _meshMaterials[index] = material;
            mesh.materials = _meshMaterials.Where(t => t != null).ToArray();
        }

        /// <summary>
        /// Удаляет дополнительный материал
        /// </summary>
        public void RemoveAdditionalMaterial(int index = 1)
        {
           
            _meshMaterials[index] = null;
            mesh.materials = _meshMaterials.Where(t => t != null).ToArray();
        }

        /// <summary>
        /// Событие клика на игровом объекте
        /// </summary>
        

        /// <summary>
        /// Событие наведения и сброса наведения на объект
        /// </summary>
        public event FocusEventHandler OnFocusEventHandler;

       

        //При навадении на объект мышки, вызывается данный метод
        //При наведении на фишку, должна подсвечиваться клетка под ней
        //При наведении на клетку - подсвечиваться сама клетка
        public abstract void OnPointerEnter(PointerEventData eventData);

        //Аналогично методу OnPointerEnter(), но срабатывает когда мышка перестает
        //указывать на объект, соответственно нужно снимать подсветку с клетки
        public abstract void OnPointerExit(PointerEventData eventData);

        //При нажатии мышкой по объекту, вызывается данный метод
        

        //Этот метод можно вызвать в дочерних классах (если они есть) и тем самым пробросить вызов
        //события из дочернего класса в родительский
        protected void CallBackEvent(CellComponent target, bool isSelect)
        {
            OnFocusEventHandler?.Invoke(target, isSelect);
        }

        private void Awake()
        {
            mesh = GetComponent<MeshRenderer>();
            focusMaterial = Resources.Load<Material>("Material/focus");
            //Этот список будет использоваться для набора материалов у меша,
            //в данном ДЗ достаточно массива из 3 элементов
            //1 элемент - родной материал меша, он не меняется
            //2 элемент - материал при наведении курсора на клетку/выборе фишки
            //3 элемент - материал клетки, на которую можно передвинуть фишку
           // _meshMaterials[0] = _mesh.material;
        }

        private void Start()
        {
            _meshMaterials[0] = mesh.material;
        }
    }

    public enum ColorType
    {
        White,
        Black
    }

    public delegate void FocusEventHandler(CellComponent component, bool isSelect);
}