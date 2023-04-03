using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : GSingleton<SoundManager>
{
    AudioSource[] _audioSources = new AudioSource[(int)Sound.MaxCount];
    // DontDestroyOnLoad로 로드되는 사운드매니저에 아무생각없이 Dictionary에 지역을 이동할때마다 캐싱하게 된다면 메모리 누수가 생기기 때문에 Clear함수로 메모리를 관리해야한다.
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    // 볼륨 값을 저장할 변수
    private float[] _volumeValues = new float[(int)Sound.MaxCount] { 1f, 1f, 1f };

    //오디오 재생기    ->AudioSource
    //오디오 음원      ->AudioClip
    //오디오를 듣는곳  ->AudioListener


    //! 오디오 소스를 담아둘 빈 게임 오브젝트를 생성해서 타입이름에 해당하는 오브젝트에 오디오소스를 넣어주는 코드
    protected override void Init()
    {
        base.Init();
        GameObject root = GameObject.Find("@Sound");
        if (root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            string[] SoundNames = System.Enum.GetNames(typeof(Sound));
            for (int i = 0; i < SoundNames.Length - 1; i++)
            { 
                GameObject gameObject_ = new GameObject { name= SoundNames[i] };
                _audioSources[i] = gameObject_.AddComponent<AudioSource>();
                gameObject_.transform.parent = root.transform;
            }

            _audioSources[(int)Sound.Bgm].loop = true;
        }
    }

    //! 오디오 소스 딕셔너리 안에 들어있는 오디오 소스를 모두 초기화해서 비워준다.
    public void Clear()
    { 
        foreach (AudioSource audioSource in _audioSources)
        { 
            audioSource.clip = null;
            audioSource.Stop(); 
        }
        _audioClips.Clear();
    }

    // [KJH] ADD
    //! 오디오의 재생을 종료하기 위한 함수
    public void Stop(Sound type = Sound.UI_SFX, float pitch = 1.0f)
    {
        if (type == Sound.Bgm)
        {
            AudioSource audioSource = _audioSources[(int)Sound.Bgm];
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
        else if (type == Sound.SFX)
        {
            AudioSource audioSource = _audioSources[(int)Sound.SFX];
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
        else
        {
            AudioSource audioSource = _audioSources[(int)Sound.UI_SFX];
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    //! 랩핑한 오디오 클립을 받는 버전의 코드
    public void Play(string path, Sound type = Sound.UI_SFX, float pitch = 1.0f)
    {
        AudioClip audioClip = GetAudioClip(path , type);
        Play(audioClip, type, pitch);
    }

    //! DB에서 받을때는 경로로 받지만 그전까지 귀찮으니 오디오 클립으로 받는다.
    public void Play(AudioClip audioClip, Sound type = Sound.UI_SFX, float pitch = 1.0f)
    {


        if (type == Sound.Bgm)
        {
            if (audioClip == null)
            {
                return;
            }

            AudioSource audioSource = _audioSources[(int)Sound.Bgm];
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.volume = _volumeValues[(int)Sound.Bgm];
            audioSource.Play();
        }
        else if (type == Sound.SFX)
        {
            AudioSource audioSource = _audioSources[(int)Sound.SFX];
            audioSource.pitch = pitch;
            audioSource.volume = _volumeValues[(int)Sound.SFX];
            audioSource.PlayOneShot(audioClip);
        }
        else
        {
            AudioSource audioSource = _audioSources[(int)Sound.UI_SFX];
            audioSource.pitch = pitch;
            audioSource.volume = _volumeValues[(int)Sound.UI_SFX];
            audioSource.PlayOneShot(audioClip);
        }
    }

    //! 오디오 클립의 경로를 만들어주거나 파일을 로드해오는 함수
    AudioClip GetAudioClip(string path, Sound type = Sound.UI_SFX)
    {
        // 사운드 경로가 생략되었다면 사운드 경로를 Resources/Audio로 생성해준다!
        if (path.Contains("Audio/") == false)
        {
            path = $"Audio/{path}";
        }

        AudioClip audioClip = null;

        if (type == Sound.Bgm)
        {
            audioClip = Resources.Load<AudioClip>(path);
        }
        else
        {
            if (_audioClips.TryGetValue(path, out audioClip) == false)
            {
                audioClip = Resources.Load<AudioClip>(path);
                _audioClips.Add(path, audioClip);
            }
        }
        if (audioClip == null)
        {
            Debug.Log($"AudioClip Missing ! {path}");
        }

        return audioClip;
    }

    // 각 오디오 타입별 볼륨을 설정하는 메소드 추가
    public void SetVolume(Sound type, float volume)
    {
        _volumeValues[(int)type] = Mathf.Clamp01(volume);
        _audioSources[(int)type].volume = _volumeValues[(int)type];
    }

    public float GetVolume(Sound type)
    {
        return _volumeValues[(int)type];
    }

}
